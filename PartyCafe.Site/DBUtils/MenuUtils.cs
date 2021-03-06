﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class MenuItemView
    {
        public int idRecord;
        public string name;
        public string price;
        public string photoPath;
        public string description;
        public string Weight;
        public string Country;
        public string Platform;
    }

    public class MenuGroupView
    {
        public int idRecord;
        public string name;
        public string photoPath;
        public string hashtag;
        public List<MenuItemView> items;
        public List<MenuGroupView> subGroups;
    }

    public class PCMenuItem
    {
        public int IdRecord;
        public string Name;
        public float Price;
        public string Country;
        public string Description;
        public string Platform;
        public string Weight;
        public int IdGroup;
    }
    public class PCMenuGroup
    {
        public int IdRecord;
        public string Name;
        public int? IdParent;
        public int IdPhoto;
    }

    public static class MenuUtils
    {
        private class MenuItemData
        {
            public int idRecord;
            public int idGroup;
            public string name;
            public decimal price;
            public string photoPath;
            public string hashtag;
            public string description;
            public string Weight;
            public string Country;
            public string Platform;
        }

        private class MenuGroupData
        {
            public int idRecord;
            public string name;
            public int idParent;
            public string photoPath;
            public string hashtag;
        }

        private static List<MenuItemData> itemsData;
        private static List<MenuGroupData> groupsData;
        private static object _fillDataLock = new Object();


        public static List<MenuGroupView> GetMainGroups()
        {
            var db = MainUtils.GetDBContext();
            return (from g in db.MenuGroups
                          join p in db.Photos on g.IdPhoto equals p.IdRecord
                          select new MenuGroupView()
                          {
                              idRecord = g.IdRecord,
                              name = g.GroupName,
                              photoPath = PhotoUtils.GetRelativeUrl(p.Path),
                              //hashtag = p.Hashtag,
                          }).ToList();
        }

        /*
        public static MenuGroupView GetGroupById(int id)
        {
            var db = MainUtils.GetDBContext();
            var data =  db.MenuGroups.Single(x => x.IdPhoto == id);
            var result = new MenuGroupView()
            {
                idRecord = data.IdRecord,
                name = data.GroupName,
                photoPath = PhotoUtils.GetRelativeUrl(data.Photo.Path),
                //hashtag = data.Photo.Hashtag,
                //items = data.MenuItems.Select(x => new )
            };
         
              select new MenuGroupView()
                        {
                            idRecord = g.IdRecord,
                            name = g.GroupName,
                            photoPath = PhotoUtils.GetRelativeUrl(p.Path),
                            hashtag = p.Hashtag,
                        }
           
           
            result.items = (from item in db.MenuItems
                            join photo in db.Photos on item.IdPhoto equals photo.IdRecord
                            where item.IdGroup == id
                            select new MenuItemView()
                            {
                                
                            })
            
        }
    */


        public static List<MenuGroupView> GetAll()
        {
            List<MenuGroupView> menu = new List<MenuGroupView>();

            lock (_fillDataLock)
            { 
                FillData();

                foreach (var el in groupsData)
                {
                    if (el.idParent == 0)
                    {
                        MenuGroupView md = new MenuGroupView();
                        md.idRecord = el.idRecord;
                        md.name = el.name;
                        md.photoPath = el.photoPath;

                        menu.Add(md);
                    }
                }

                foreach (var el in menu)
                {
                    el.items = GetItems(el.idRecord);
                    el.subGroups = GetGroups(el.idRecord);
                }
            }

            return menu;
        }

        private static List<MenuGroupView> GetGroups(int idGroup)
        {
            /*
                Создаем группу, наполняем ее данными по итемс
                наполняем ее данными по группам
            */
            List<MenuGroupView> result = new List<MenuGroupView>();
            foreach (var elem in groupsData)
            {
                if (idGroup == elem.idParent)
                {
                    var newGroup = new MenuGroupView();

                    newGroup.name = elem.name;
                    newGroup.photoPath = elem.photoPath;
                    newGroup.idRecord = elem.idRecord;
                    newGroup.items = GetItems(elem.idRecord);
                    newGroup.subGroups = GetGroups(elem.idRecord);

                    result.Add(newGroup);
                }
            }
            return result;
        }

        private static List<MenuItemView> GetItems(int idGroup)
        {
            var result = new List<MenuItemView>();
            foreach(var item in itemsData)
            {
                if (item.idGroup != idGroup) continue;

                MenuItemView elem = new MenuItemView();
                elem.idRecord = item.idRecord;
                elem.name = item.name;
                elem.photoPath = item.photoPath;
                elem.price = item.price.ToString();
                elem.Country = item.Country;
                elem.description = item.description;
                elem.Platform = item.Platform;
                elem.Weight = item.Weight;
                result.Add(elem);
            }
            return result;
        }

        private static void FillData()
        {
            var db = MainUtils.GetDBContext();  
            var items = (from i in db.MenuItems
                         join p in db.Photos on i.IdPhoto equals p.IdRecord
                         select new { i.IdRecord, i.Name, i.IdGroup, i.description, i.Country, i.Platform, i.Price, i.Weight, p.Path, /*p.Hashtag*/ }).ToList();

            if (itemsData == null)
            {
                itemsData = new List<MenuItemData>();
            }
            else
            {
                itemsData.Clear();
            }

            foreach (var item in items)
            {
                MenuItemData id = new MenuItemData();
                id.idRecord = item.IdRecord;
                id.name = item.Name;
                id.photoPath = PhotoUtils.GetRelativeUrl(item.Path);
                //id.hashtag = item.Hashtag;
                id.Platform = item.Platform; 
                id.price = item.Price;
                id.Weight = item.Weight;
                id.description = item.description;
                id.Country = item.Country;
                id.idGroup = item.IdGroup;

                itemsData.Add(id);
            }


            var groups = (from g in db.MenuGroups
                          join p in db.Photos on g.IdPhoto equals p.IdRecord
                          select new { g.IdRecord, g.IdParent, g.GroupName, p.Path/*, p.Hashtag*/ }).ToList();

            if (groupsData == null)
            {
                groupsData = new List<MenuGroupData>();
            }
            else
            {
                groupsData.Clear();
            }

            foreach(var group in groups)
            {
                MenuGroupData gd = new MenuGroupData();
                gd.idParent = (group.IdParent == null) ? 0 : (int)group.IdParent;
                gd.idRecord = group.IdRecord;
                gd.name = group.GroupName;
                gd.photoPath = PhotoUtils.GetRelativeUrl(group.Path);
                //gd.hashtag = group.Hashtag;

                groupsData.Add(gd);
            }
        }

        public static void InsertItem (PCMenuItem partyItem, PCPhoto image, string userCreate)
        {
            var newMenuItem = new MenuItems();
            newMenuItem.Name = partyItem.Name ?? string.Empty;
            newMenuItem.description = partyItem.Description ?? string.Empty;
            newMenuItem.Platform = partyItem.Platform ?? string.Empty;
            newMenuItem.Price = Convert.ToDecimal(partyItem.Price);
            newMenuItem.Country = partyItem.Country ?? string.Empty;
            newMenuItem.IdGroup = partyItem.IdGroup;
            newMenuItem.Weight = partyItem.Weight;

            if (image != null)
            {
                newMenuItem.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newMenuItem.IdPhoto = 0;
            }
            //if (image.hashtag != null & newMenuItem.IdPhoto != 0)
            //    PhotoUtils.Edithashtag(newMenuItem.IdPhoto, image.hashtag);

            newMenuItem.DateCreate = DateTime.Now;
            newMenuItem.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.MenuItems.InsertOnSubmit(newMenuItem);
            dbContext.SubmitChanges();
        }

        public static void EditItem(PCMenuItem partyItem, PCPhoto image, string userUpdate)
        {
            var dbContext = MainUtils.GetDBContext();
            var curMenuItem = (from mi in dbContext.MenuItems
                            where mi.IdRecord == partyItem.IdRecord
                            select mi).SingleOrDefault();

            curMenuItem.Name = partyItem.Name ?? string.Empty;
            curMenuItem.description = partyItem.Description ?? string.Empty;
            curMenuItem.Platform = partyItem.Platform ?? string.Empty;
            curMenuItem.Price = Convert.ToDecimal(partyItem.Price);
            curMenuItem.Country = partyItem.Country ?? string.Empty;
            curMenuItem.IdGroup = partyItem.IdGroup;
            curMenuItem.Weight = partyItem.Weight;

            curMenuItem.DateUpdate = DateTime.Now;
            curMenuItem.UserUpdate = userUpdate;

            if (image != null)
            { 
                if (curMenuItem.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curMenuItem.IdPhoto, image, userUpdate);
                } else {
                    curMenuItem.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }
            //if (image.hashtag != null && curMenuItem.IdPhoto != 0)
            //    PhotoUtils.Edithashtag(curMenuItem.IdPhoto, image.hashtag);

            dbContext.SubmitChanges();
        }

        public static void DelItem(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curMenuItem = (from mi in dbContext.MenuItems
                            where mi.IdRecord == idRecord
                            select mi).SingleOrDefault();

            dbContext.MenuItems.DeleteOnSubmit(curMenuItem);
            dbContext.SubmitChanges();

            if (curMenuItem.IdPhoto > 0) { PhotoUtils.DelImage(curMenuItem.IdPhoto); };
        }

        public static void InsertGroup(PCMenuGroup partyGroup, PCPhoto image, string userCreate)
        {
            var newMenuGroup = new MenuGroups();
            newMenuGroup.GroupName = partyGroup.Name ?? string.Empty;
            newMenuGroup.IdParent = partyGroup.IdParent;

            if (image != null)
            {
                newMenuGroup.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newMenuGroup.IdPhoto = 0;
            }
            //if (image.hashtag != null && newMenuGroup.IdPhoto != 0)
            //    PhotoUtils.Edithashtag(newMenuGroup.IdPhoto, image.hashtag);


            newMenuGroup.DateCreate = DateTime.Now;
            newMenuGroup.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.MenuGroups.InsertOnSubmit(newMenuGroup);
            dbContext.SubmitChanges();
        }

        public static void EditGroup(PCMenuGroup partyGroup, PCPhoto image, string userUpdate)
        {
            var dbContext = MainUtils.GetDBContext();
            var curMenuGroup = (from mg in dbContext.MenuGroups
                               where mg.IdRecord == partyGroup.IdRecord
                               select mg).SingleOrDefault();

            curMenuGroup.GroupName = partyGroup.Name ?? string.Empty;
            curMenuGroup.IdParent = partyGroup.IdParent;

            curMenuGroup.DateUpdate = DateTime.Now;
            curMenuGroup.UserUpdate = userUpdate;

            if (image != null)
            {
                if (curMenuGroup.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curMenuGroup.IdPhoto, image, userUpdate);
                }
                else
                {
                    curMenuGroup.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }
            //if (image.hashtag != null && curMenuGroup.IdPhoto != 0)
            //    PhotoUtils.Edithashtag(curMenuGroup.IdPhoto, image.hashtag);

            dbContext.SubmitChanges();
        }

        public static void DelGroup(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGroup = (from mg in dbContext.MenuGroups
                            where mg.IdRecord == idRecord
                            select mg).SingleOrDefault();

            var groupsToDel = curGroup.MenuGroups2.ToList();
            foreach(var groupDel in groupsToDel)
            {
                DelGroup(groupDel.IdRecord);
            }

            var itemsToDel = curGroup.MenuItems.ToList();
            foreach (var item in itemsToDel)
            {
                DelItem(item.IdRecord);
            }


            dbContext.MenuGroups.DeleteOnSubmit(curGroup);
            dbContext.SubmitChanges();

            if (curGroup.IdPhoto > 0) { PhotoUtils.DelImage(curGroup.IdPhoto); };
        }
    }
}