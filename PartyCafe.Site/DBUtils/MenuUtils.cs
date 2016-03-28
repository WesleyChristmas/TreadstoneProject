using System;
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
        public List<MenuItemView> items;
        public List<MenuGroupView> subGroups;
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
        }

        private static List<MenuItemData> itemsData;
        private static List<MenuGroupData> groupsData;

        public static List<MenuGroupView> GetAll()
        {
            FillData();

            List <MenuGroupView> menu = new List<MenuGroupView>();

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
                if (item.idGroup == idGroup)
                {
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
            }
            return result;
        }

        private static void FillData()
        {
            var db = MainUtils.GetDBContext();  
            var items = (from i in db.MenuItems
                         join p in db.Photos on i.IdPhoto equals p.IdRecord
                         select new { i.IdRecord, i.Name, i.IdGroup, i.description, i.Country, i.Platform, i.Price, i.Weight, p.Path }).ToList();

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
                id.photoPath = item.Path;
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
                          select new { g.IdRecord, g.IdParent, g.GroupName, p.Path }).ToList();

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
                gd.photoPath = group.Path;

                groupsData.Add(gd);
            }
        }
    }
}