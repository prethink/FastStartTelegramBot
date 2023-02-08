using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using TelegramBotFastStart.Helpers;
using TelegramBotFastStart.Models.DataBase.Enums;

namespace TelegramBotFastStart.Models.DataBase
{
    [Table("Advertising")]
    public class Advertising
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("media")]
        public string? Media { get; set; }
        [Column("text")]
        public string Text { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("menu_data")]
        public string? MenuData { get; set; }
        [Column("menu_type")]
        public MenuType MenuType { get; set; }
        [Column("message_type")]
        public MessageType MessageType { get; set; }
        [Column("start_age")]
        public long? StartAge { get; set; }
        [Column("end_age")]
        public long? EndAge { get; set; }
        [Column("tags")]
        public string? Tags { get; set; }
        [Column("viewed")]
        public long Viewed { get; set; }

        public List<InlineURL> GetMenu()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<InlineURL>>(MenuData ?? "");
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return new List<InlineURL>();
            }

        }

        public static string WriteMenu(List<IInlineContent> menu)
        {
            try
            {
                return JsonConvert.SerializeObject(menu);
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return "";
            }
        }

    }

    public class MenuItem
    {
        public IInlineContent Type { get; set; }
        public string Text { get; set; }
    }
}
