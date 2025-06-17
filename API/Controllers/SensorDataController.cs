using API.DTOs;
using Core.FingerId; // افترضنا أن هذا يحتوي على تعريف SensorData
using Microsoft.AspNetCore.Http; // قد لا تكون ضرورية إذا كان BaseApiController لا يستخدمها مباشرة
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Project.APIS.Erorrs; // هذا الاستيراد لم يعد موجودًا في الكود الجديد، لذلك أزيل
using Repository.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic; // مطلوب لـ List<SensorDataDTO>

namespace API.Controllers
{
    [ApiController] // إضافة لتفعيل سلوكيات API تلقائية
    [Route("api/[controller]")] // تحديد مسار الكنترولر الافتراضي
    public class SensorDataController : BaseApiController // بقيت ترث من BaseApiController كما في الكود الأصلي
    {
        private readonly StoreContext _storeContext;

        public SensorDataController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        // POST api/SensorData (عاد ليقبل ID و Timestamp كما في الكود الأول)
        [HttpPost]
        public async Task<ActionResult<SensorData>> PostData(SensorDataDTO sensorData)
        {
            // لا يوجد تحقق من الاسم، نعود للطريقة الأصلية
            var mappedData = new SensorData
            {
                ID = sensorData.ID, // استخدام ID من الـ DTO
                Timestamp = sensorData.Timestamp // استخدام Timestamp من الـ DTO
            };

            _storeContext.SensorData.Add(mappedData);
            await _storeContext.SaveChangesAsync();

            return Ok(mappedData); // إرجاع الكائن بالكامل
        }

        // GET api/SensorData/{id} (يستخدم FirstOrDefaultAsync ومعالجة NotFound)
        [HttpGet("{id}")] // تحديد أن الـ ID سيكون جزءًا من المسار
        public async Task<ActionResult<SensorDataDTO>> GetData(int id)
        {
            var data = await _storeContext.SensorData
                                .Where(x => x.ID == id)
                                .OrderByDescending(x => x.Timestamp) // إذا كان هناك أكثر من واحد بنفس الـ ID (غير متوقع إذا كان ID مفتاحًا أساسيًا)
                                .FirstOrDefaultAsync(); // يجلب أول كائن مطابق أو null

            if (data == null)
                return NotFound(); // 404 Not Found إذا لم يتم العثور على بيانات

            // تحويل الكيان إلى DTO قبل الإرجاع
            var dto = new SensorDataDTO
            {
                ID = data.ID,
                Timestamp = data.Timestamp // تعيين Timestamp هنا
            };

            return Ok(dto);
        }

        // GET api/SensorData/last-id (نقطة نهاية جديدة: للحصول على آخر ID)
        [HttpGet("last-id")]
        public async Task<ActionResult<int>> GetLastId()
        {
            var lastId = await _storeContext.SensorData
                                .OrderByDescending(x => x.ID)
                                .Select(x => x.ID)
                                .FirstOrDefaultAsync(); // يجلب آخر ID أو 0 إذا كان الجدول فارغًا

            return Ok(lastId);
        }

        // GET api/SensorData (نقطة نهاية جديدة: للحصول على كل البيانات)
        [HttpGet]
        public async Task<ActionResult<List<SensorDataDTO>>> GetAllData()
        {
            var dataList = await _storeContext.SensorData
                                .OrderByDescending(x => x.Timestamp) // ترتيب حسب الأحدث
                                .Select(x => new SensorDataDTO // تحويل للـ DTO مباشرة
                                {
                                    ID = x.ID,
                                    Timestamp = x.Timestamp // استخدام Timestamp هنا
                                })
                                .ToListAsync(); // جلب كل السجلات

            return Ok(dataList);
        }

        // POST api/SensorData/clear (نقطة نهاية جديدة: لمسح جميع البيانات)
        [HttpPost("clear")]
        public async Task<IActionResult> ClearData()
        {
            // حذف جميع السجلات من جدول SensorData
            _storeContext.SensorData.RemoveRange(_storeContext.SensorData);
            await _storeContext.SaveChangesAsync();
            return Ok(); // إرجاع 200 OK
        }

        // GET api/SensorData/generate-id (نقطة نهاية جديدة: لتوليد ID مقترح)
        [HttpGet("generate-id")]
        public async Task<ActionResult<int>> GenerateId()
        {
            // الحصول على آخر ID موجود، أو 0 إذا لا يوجد بيانات
            var lastId = await _storeContext.SensorData
                .OrderByDescending(x => x.ID)
                .Select(x => x.ID)
                .FirstOrDefaultAsync();

            int newId = lastId + 1; // ID الجديد هو آخر ID + 1
            return Ok(newId);
        }

        // GET api/SensorData/count (نقطة نهاية جديدة: لعد عدد السجلات)
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetSensorDataCount() // تم تغيير الاسم ليعكس المحتوى
        {
            var count = await _storeContext.SensorData.CountAsync();
            return Ok(count);
        }
    }
}
