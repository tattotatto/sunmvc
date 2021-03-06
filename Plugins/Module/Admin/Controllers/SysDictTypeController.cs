﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sun.Framework.Login;
using Sun.BaseOperate.Interface;
using Sun.Model.DB;
using Sun.BaseOperate.DbContext;
using Sun.Model.Common;

namespace Plugin.Admin.Controllers
{
    [AdminModule]
    public class SysDictTypeController  : AdminBaseController
    {
        IDictOp DbOp;
        public SysDictTypeController(IDictOp op)
        {
            DbOp = op;
        }
        public ActionResult Index()
        {
            ViewData["limitsStr"] = LimitsStr;
            return View();
        }
        //获取字典分页列表
        public JsonResult GetDictPageJson(int page = 1, int rows = 30)
        {
            JsonResult result = new JsonResult();
            if (Limits.Contains(1))
            {
                GridPage<Dict> data = new GridPage<Dict>();
                var list = DbOp.GetDictPageList(0, page, rows);
                data.total = list.TotalItems;
                data.rows = list.Items;
                result.Data = data;
            }
            return result;
        }
        //新增或修改字典数据
        public string EditOrUpdateDict(Dict dictInfo)
        {
            if (dictInfo.DictId != 0)
            {
                if (Limits.Contains(2))
                {
                    DbOp.Update(dictInfo);
                }
                else
                    return "你没有权限进行修改";
            }
            else
            {
                if (Limits.Contains(3))
                {
                    dictInfo.IsUsable = true;
                    dictInfo.TypeId = 0;
                    DbOp.Add(dictInfo);
                }
                else
                    return "你没有权限新增数据";
            }
            return "True";
        }
        //删除字典数据
        public string DeleteDict(int dictId)
        {
            if (Limits.Contains(4))
            {
                try
                {
                    DbOp.Delete(dictId);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "True";
        }
	}
}