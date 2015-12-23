using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ExaminationManager
    {
        private static List<Examination> _ExaminationCache = new List<Examination>();
        private static object _ExaminationQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ExaminationCache = db.Examinations.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<Examination> GetAll()
        {
            return _ExaminationCache.OrderBy(t => t.Examination_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<Examination> GetPagedList(int pageNumber, int pageSize)
        {
            return _ExaminationCache.OrderBy(a => a.Examination_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static Examination Get(string id)
        {
            return _ExaminationCache.FirstOrDefault(p => p.Examination_Id == id);
        }

        //新增單一記錄
        public static void Create(Examination Examination)
        {
            Create(new List<Examination>() { Examination });
        }

        //新增多筆記錄
        public static void Create(List<Examination> Examinations)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ExaminationQueueLock)
                {
                    db.Examinations.AddRange(Examinations);
                    db.SaveChanges();
                    //更新記憶体
                    _ExaminationCache.AddRange(Examinations);
                }
            }
        }

        //更新一筆記錄
        public static void Update(Examination Examinations)
        {
            Update(new List<Examination>() { Examinations });
        }

        //更新多筆記錄
        public static void Update(List<Examination> Examinations)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = Examinations.Select(a => a.Examination_Id).ToList();
                var objInDB = db.Examinations.Where(a => objIDs.Contains(a.Examination_Id)).ToList();

                foreach (Examination item in objInDB)
                {
                    var theNewFromOutside = Examinations.FirstOrDefault(a => a.Examination_Id == item.Examination_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ExaminationQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ExaminationCache.RemoveAll(a => objIDs.Contains(a.Examination_Id));
                    _ExaminationCache.AddRange(Examinations);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(Examination Examination)
        {
            Remove(new List<Examination>() { Examination });
        }

        //刪除多筆記錄
        public static void Remove(List<Examination> Examinations)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = Examinations.Select(a => a.Examination_Id).ToList();
                var objInDB = db.Examinations.Where(a => objIDs.Contains(a.Examination_Id)).ToList();
                
                lock (_ExaminationQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in Examinations)
                    {
                        _ExaminationCache.Remove(item);
                    }
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static ExaminationModel DomainToModel(Examination Examination)
        {
            ExaminationModel viewModel = new ExaminationModel();
            Mapper.CreateMap<Examination, ExaminationModel>();
            viewModel = Mapper.Map<Examination, ExaminationModel>(Examination);

            return viewModel;
        }

        public static Examination ModelToDomain(ExaminationModel viewModel)
        {
            Examination Examination = new Examination();

            try
            {
                Mapper.CreateMap<ExaminationModel, Examination>();
                Examination = Mapper.Map<ExaminationModel, Examination>(viewModel);
            }
            catch (Exception e)
            {

            }


            return Examination;
        }

        #endregion
    }
}