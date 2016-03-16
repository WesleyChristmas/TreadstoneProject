using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PCGame
    {
        public int idRecord;
        public string name;
        public string platform;
        public string description;
        public int idPhoto;
        public string photoPath;
    }

    public static class GameUtils
    {
        public static List<PCGame> GetAll()
        {
            var dbContext = MainUtils.GetDBContext();
            var games = (from e in dbContext.Games
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          select new { e.IdRecord, e.Name, e.IdPhoto, e.Platform, e.Description, p.Path }).ToList();

            List<PCGame> resultList = new List<PCGame>();
            foreach (var e in games)
            {
                PCGame pcGame = new PCGame();

                pcGame.idRecord = e.IdRecord;
                pcGame.name = e.Name;
                pcGame.idPhoto = e.IdPhoto;
                pcGame.photoPath = e.Path;
                pcGame.description = e.Description;
                pcGame.platform = e.Platform;

                resultList.Add(pcGame);
            }

            return resultList;
        }

        public static void InsertGame(PCGame game, string userCreate, PCPhoto image)
        {
            var newGame = new Games();
            newGame.Name = game.name != null ? game.name : "";
            newGame.Description = game.description != null ? game.description : "";
            newGame.Platform = game.platform != null ? game.platform : "";

            if (image != null)
            {
                newGame.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newGame.IdPhoto = 0;
            }

            newGame.DateCreate = DateTime.Now;
            newGame.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Games.InsertOnSubmit(newGame);
            dbContext.SubmitChanges();
        }

        public static void EditGame(PCGame game, string userUpdate, PCPhoto image)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGame = (from e in dbContext.Games
                            where e.IdRecord == game.idRecord
                            select e).SingleOrDefault();

            curGame.Name = game.name != null ? game.name : "";
            curGame.Description = game.description != null ? game.description : "";
            curGame.Platform = game.platform != null ? game.platform : "";

            curGame.DateUpdate = DateTime.Now;
            curGame.UserUpdate = userUpdate;

            if (image != null)
            {
                if (curGame.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curGame.IdPhoto, image, userUpdate);
                } else {
                    curGame.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }

            dbContext.SubmitChanges();
        }

        public static void DelGame(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGame = (from e in dbContext.Games
                            where e.IdRecord == idRecord
                            select e).SingleOrDefault();

            dbContext.Games.DeleteOnSubmit(curGame);
            dbContext.SubmitChanges();

            if (curGame.IdPhoto > 0) { PhotoUtils.DelImage(curGame.IdPhoto); };
        }
    }
}