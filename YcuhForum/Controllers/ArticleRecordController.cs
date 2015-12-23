using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            if (catchData == null)
            {
                InitModel(ref viewData, id);
                return PartialView("列表模板", viewData);
            }
            else if(catchData.ArticleUserRecord_UpdateTime == new DateTime())
            {
                ArticleUserRecordManager.ModelToDomain(viewData);
                return PartialView("列表模板", viewData);
            }
               
            else
            {
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

                if (articleUserRecordModel.ArticleUserRecord_UpdateTime == new DateTime())
                {
                    newCreateModel(ref articleUserRecordModel);
                    var articleUserRecordObj = ArticleUserRecordManager.ModelToDomain(articleUserRecordModel);
                    ArticleUserRecordManager.Update(articleUserRecordObj);
                }
                else
                {
                    newCreateModel(ref articleUserRecordModel);
                    var articleUserRecordObj = ArticleUserRecordManager.ModelToDomain(articleUserRecordModel);
                    ArticleUserRecordManager.Create(articleUserRecordObj);
                }
                #endregion


                #region 計算點數(表未定)
                //PointCalculate(articleUserRecordModel.ArticleUserRecord_FK_ArticleId);
                #endregion


                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
              
        }


        private void PointCalculate(string article)
        {
            var userPointObj = UserPointManager.Get(strUserId);
            var articleData = ArticleManager.Get(article);
          
        }


        private void InitModel(ref ArticleUserRecordModel articleUserRecordModel, string article)
        {
            articleUserRecordModel.ArticleUserRecord_Id = Guid.NewGuid().ToString();
            articleUserRecordModel.ArticleUserRecord_FK_ArticleId = article;
            articleUserRecordModel.ArticleUserRecord_FK_UserId = strUserId;
            var articleData  = ArticleManager.Get(article);
            if (articleData != null)
            {
                //articleUserRecordModel.ArticleUserRecord_ArticleCategory = articleData.Article_FK_PointCategoryId;
                //articleUserRecordModel.ArticleUserRecord_ArticleGroup = articleData.Article_FK_ArticleGroupId;
                //articleUserRecordModel.ArticleUserRecord_ArticleTitle = articleData.Article_Title;
            }
           
        }

        private void newCreateModel(ref ArticleUserRecordModel articleUserRecordModel)
        {
            if (articleUserRecordModel.ArticleUserRecord_UpdateTime == new DateTime())
            {
                articleUserRecordModel.ArticleUserRecord_UpdateTime = DateTime.Now;
            }
            else
            {
                articleUserRecordModel.ArticleUserRecord_CreateTime = DateTime.Now;
                articleUserRecordModel.ArticleUserRecord_UpdateTime = DateTime.Now;
            }
        }
    }
}
