using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleRecordController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult GetView(string id)
        {
            var catchData = ArticleUserRecordManager.getRecordByArticelAndUser(id, strUserId);

            ArticleUserRecordModel viewData = new ArticleUserRecordModel();
            //有無顯示功能
            ViewBag.ShowReplayFunction = true;
            if (catchData == null)
            {
                return PartialView("列表模板", viewData);
            }
            else if (catchData.ArticleUserRecord_IsEnforce && catchData.ArticleUserRecord_UpdateTime == new DateTime())
            {
                viewData = ArticleUserRecordManager.DomainToModel(catchData);
                return PartialView("列表模板", viewData);
            }
            else
            {
                ViewBag.ShowReplayFunction = false;
                return PartialView("列表模板");
            }
        }

        //ajax來回
        public ActionResult Create(ArticleUserRecordModel articleUserRecordModel)
        {
            if (String.IsNullOrEmpty(articleUserRecordModel.ArticleUserRecord_Id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                #region 檢查記錄
                if (articleUserRecordModel.ArticleUserRecord_IsEnforce)
                {
                    var articleUserRecordObj = ArticleUserRecordManager.ModelToDomain(articleUserRecordModel);
                    ArticleUserRecordManager.Update(articleUserRecordObj);
                }
                else
                {
                    var articleUserRecordObj = ArticleUserRecordManager.ModelToDomain(articleUserRecordModel);
                    articleUserRecordObj.ArticleUserRecord_CreateTime = DateTime.Now;
                    articleUserRecordObj.ArticleUserRecord_UpdateTime = DateTime.Now;
                    ArticleUserRecordManager.Create(articleUserRecordObj);
                }
                #endregion
                
                #region 計算點數
                PointCalculate(articleUserRecordModel.ArticleUserRecord_FK_ArticleId);
                #endregion

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                //寫log
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "觀看紀錄新增異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(articleUserRecordModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
              
        }


        private void PointCalculate(string article)
        {
            var userPointObj = UserPointManager.Get(strUserId);
            var articleData = ArticleManager.Get(article);
            try
            {
                if (userPointObj == null)
                {
                    PointType newPointType = new PointType();
                    newPointType.PointType_Id = articleData.Article_FK_PointCategoryId;
                    newPointType.PointType_Point = articleData.Article_Point;

                    UserPoint newUserPoint = new UserPoint();
                    newUserPoint.UserPoint_Id = Guid.NewGuid().ToString();
                    newUserPoint.UserPoint_CreateTime = DateTime.Now;
                    newUserPoint.UserPoint_UpdateTime = DateTime.Now;
                    newUserPoint.UserPoint_FK_UserId = strUserId;
                    newUserPoint.UserPoint_Point = Newtonsoft.Json.JsonConvert.SerializeObject(newPointType);
                }
                else
                {
                    var pointTypeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PointType>>(userPointObj.UserPoint_Point);
                    var oldPointType = pointTypeList.Where(a => a.PointType_Id == articleData.Article_FK_PointCategoryId).FirstOrDefault();
                    if (oldPointType == null)
                    {
                        PointType newPointType = new PointType();
                        newPointType.PointType_Id = articleData.Article_FK_PointCategoryId;
                        newPointType.PointType_Point = articleData.Article_Point;
                        pointTypeList.Add(newPointType);
                    }
                    else
                    {
                        oldPointType.PointType_Point += articleData.Article_Point;
                    }
                    userPointObj.UserPoint_Point = Newtonsoft.Json.JsonConvert.SerializeObject(pointTypeList);
                    userPointObj.UserPoint_UpdateTime = DateTime.Now;
                }

                UserPointManager.Update(userPointObj);
            }
            catch( Exception e)
            {
                //寫log
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "點數新增異常";
                var pointObj = Newtonsoft.Json.JsonConvert.SerializeObject(userPointObj.UserPoint_Point); 
                var actionStr = ";目標類別:"+articleData.Article_FK_PointCategoryId +";"+"目標點數:"+articleData.Article_Point+";";
                pointObj += actionStr + actionStr;
                newErrorRecord.ErrorRecord_CustomedMessage = pointObj;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
            }
          
            
        }


    
    }
}
