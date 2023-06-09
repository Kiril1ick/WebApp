﻿using WebApp.BL.Auth;
using WebApp.BL.Generel;
using WebApp.DAL;
using WebApp.DAL.Models;

namespace WebApp.BL
{
    public class DbSessionBL : IDbSessionBL
    {
        private readonly IDbSessionDAL dbSessionDAL;
        private readonly IWebCookie webCookie;
        public DbSessionBL(IDbSessionDAL dbSessionDAL, IWebCookie webCookie)
        {
            this.dbSessionDAL = dbSessionDAL;
            this.webCookie = webCookie;
        }
        private void CreateSessionCookie(Guid sessionid)
        {
            this.webCookie.Delete(AuthConstants.SessionCookieName);
            this.webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.Now,
                LastAccessed = DateTime.Now
            };
            await dbSessionDAL.CreateSession(data);
            return data;
        }

        private SessionModel? sessionModel = null;
        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var sessionString = webCookie.Get(AuthConstants.SessionCookieName);
            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();

            var data = await this.dbSessionDAL.GetSession(sessionId);
            if (data == null)
            {
                data = await this.CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }
            sessionModel = data;
            return data;
        }

        public async Task<int> SetUserId(int userId)
        {
            var data = await this.GetSession();
            data.UserID = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            return await dbSessionDAL.CreateSession(data);
        }

        public async Task<int?> GetUserId()
        {
            var data = await this.GetSession();
            return data.UserID;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await this.GetSession();
            return data.UserID != null;
        }

        public async Task LockSession()
        {
            var data = await this.GetSession();
            await dbSessionDAL.LockSession(data.DbSessionId);
        }
    }
}
