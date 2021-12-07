using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SingleRecordController : ControllerBase
    {
        private RecordContext db;

        public SingleRecordController(RecordContext context)
        {
            db = context;
        }

        [HttpGet("{Id}")]
        public JsonResult Get(int Id)
        {
            try
            {
                Record record = db.Records.Where(r => r.Id == Id).FirstOrDefault();
                return new JsonResult(record);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Post([FromBody] Record record)
        {
            try
            {
                db.Records.Add(record);
                db.SaveChanges();
                return new JsonResult(new { Success = "True", responseText = "Creation is successful" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpPut("{Id}")]
        public JsonResult Put(int Id, [FromBody] Record newRecord)
        {
            try
            {
                Record record = db.Records.Where(r => r.Id == Id).FirstOrDefault();
                record.PatientLastName = newRecord.PatientLastName;
                record.DoctorLastName = newRecord.DoctorLastName;
                record.Priority = newRecord.Priority;
                record.MedicalExam = newRecord.MedicalExam;
                record.Date = newRecord.Date;
                db.SaveChanges();

                return new JsonResult(new { Success = "True", responseText = "Update is successful" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            try
            {
                Record record = db.Records.Where(r => r.Id == Id).FirstOrDefault();
                db.Records.Remove(record);
                db.SaveChanges();
                return new JsonResult(new {Success = "True", responseText = "Deletion is successful"});
            }
            catch (Exception ex)
            {
                return new JsonResult(new {Success = "False", responseText = ex.Message});
            }
        }
    }
}
