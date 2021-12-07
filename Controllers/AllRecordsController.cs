using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllRecordsController : ControllerBase
    {
        private RecordContext db;

        public AllRecordsController(RecordContext context)
        {
            db = context;
        }

        [HttpGet]
        public JsonResult GetAllRecords()
        {
            try
            {
                var records = db.Records;
                return new JsonResult(records);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpDelete]
        public JsonResult Post(int [] ids)
        {
            try
            {
                var records = db.Records.Where(r => ids.Contains(r.Id));
                db.Records.RemoveRange(records);
                db.SaveChanges();
                return new JsonResult(new { Success = "True", responseText = "Deletion is successful" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpGet("{days}")]

        public JsonResult GetByFilter(int days)
        {
            try
            {
                var records = db.Records.Where(r => Math.Abs(r.Date.DayOfYear - DateTime.Now.DayOfYear) < days);
                return new JsonResult(records);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }
    }
}
