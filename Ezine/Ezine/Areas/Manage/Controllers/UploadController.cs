﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;
using Ezine.Models;

namespace Ezine.Areas.Manage.Controllers
{
    public class UploadController : Controller
    {
        private IAttachment repository;
        public UploadController(IAttachment attachmentRepository)
        {
            repository = attachmentRepository;
        }

        // GET: Manage/Upload
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传文件，返回存储文件的Id
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadFile()
        {
            int row = 0;
            foreach (string upload in Request.Files)
            {
                if (!HasFiles.HasFile(Request.Files[upload]))
                {
                    continue;
                }
                string miniType = Request.Files[upload].ContentType;
                string fileType = Path.GetExtension(Request.Files[upload].FileName);
                Stream fileStream = Request.Files[upload].InputStream;
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                string path = HttpContext.Server.MapPath("../../UpLoadFile/");
                int size = Request.Files[upload].ContentLength;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Request.Files[upload].SaveAs(path + filename);

                DateTime nowTime = DateTime.Now;
                Random rd = new Random();
                var file = new Attachment()
                {
                    FileType = fileType,
                    FileSzie = size,
                    OldFileName = filename,
                    NewFileName = nowTime.Year.ToString() + nowTime.Month.ToString() + nowTime.Day.ToString() + nowTime.Hour.ToString()
                    + nowTime.Minute.ToString() + nowTime.Second.ToString() + rd.Next(1000, 1000000),
                    FilePath = path,
                    AddTime = nowTime
                };

                row = repository.AddFile(file);
            }

            return Json(row);
        }
    }

    public static class HasFiles
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}