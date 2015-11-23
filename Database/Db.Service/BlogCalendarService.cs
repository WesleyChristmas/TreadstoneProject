using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessEntity;
using BusinessEntity.Models;
using BusinessInterface;
using Db.Service.PrivateServices;
using Entity.Model;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Db.Service
{
    public interface IBlogCalendarService : IService<BlogCalendar>, IBlogCalendarServiceBase
    {
        
    }

    public class BlogCalendarService : Service<BlogCalendar>, IBlogCalendarService
    {
        private readonly IImageService _imageService; 
        private readonly IRepositoryAsync<BlogCalendar> _repositoryCalendar;
        private readonly IUnitOfWorkAsync _uof;

        public BlogCalendarService(IRepositoryAsync<BlogCalendar> repositoryCalendar,
            IImageService imageSerivce,
            IUnitOfWorkAsync uof)
            :base(repositoryCalendar)
        {
            _imageService = imageSerivce;
            _repositoryCalendar = repositoryCalendar;
            _uof = uof;


            Mapper.CreateMap<BlogCalendar, BlogCalendarEntity>()
                .ForMember(x => x.PhotoLink, opt => opt.MapFrom(r => r.Photo.Link));
            Mapper.CreateMap<BlogCalendarEntity, BlogCalendar>();
        }

        public SendCalendarModel GetCalendar()
        {
            var result = new SendCalendarModel
            {
                CurDate = DateTime.Now.Date
            };
            var calendarDb = _repositoryCalendar.Query()
                .Include(x => x.Photo)
                .Select()
                .Where(x => x.EventDate.Month == result.CurDate.Month ||
                            x.EventDate.Month == result.CurDate.Month + 1)
                .ToList();

            result.Calendar = Mapper.Map<List<BlogCalendar>, List<BlogCalendarEntity>>(calendarDb);

            return result;
        }

        public SendCalendarModel GetCalendarAll()
        {
            var result = new SendCalendarModel();
            var calendarDb = _repositoryCalendar.Query()
                .Include(x => x.Photo)
                .Select()
                .ToList();

            result.Calendar = Mapper.Map<List<BlogCalendar>, List<BlogCalendarEntity>>(calendarDb);

            return result;
        }

        public void AddBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image)
        {
            int? idImage = null;
            if (image != null)
            {
                idImage = _imageService.SaveImage(image);
            }

            var calendarDb = Mapper.Map<BlogCalendarEntity, BlogCalendar>(calendar);
            calendarDb.IdPhoto = idImage;
            _repositoryCalendar.Insert(calendarDb);
            _uof.SaveChanges();
        }

        public void UpdateBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image)
        {
            var updateCalendar = _repositoryCalendar.Queryable()
                .FirstOrDefault(x => x.IdRecord == calendar.IdRecord);
            if (updateCalendar == null) return;

            if (image != null)
            {
                if (updateCalendar.IdPhoto.HasValue)
                    _imageService.UpdateImage(image, updateCalendar.IdPhoto.Value);
                else
                    updateCalendar.IdPhoto = _imageService.SaveImage(image);
            }
            updateCalendar.Header = calendar.Header;
            updateCalendar.EventDate = calendar.EventDate;
            
            _repositoryCalendar.Update(updateCalendar);
            _uof.SaveChanges();
        }

        public bool DeleteBlogCalendar(int idCalendar,string serverPath)
        {
            var delCalendar = _repositoryCalendar.Queryable().FirstOrDefault(x => x.IdRecord == idCalendar);
            if (delCalendar == null || delCalendar.IdBlog.HasValue) return false;

            if(delCalendar.IdPhoto.HasValue)
                _imageService.DeleteImage(delCalendar.IdPhoto.Value, serverPath);

            _repositoryCalendar.Delete(idCalendar);
            _uof.SaveChanges();
            return true;
        }
    }
}
