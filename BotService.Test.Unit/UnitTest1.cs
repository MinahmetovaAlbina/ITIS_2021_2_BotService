using Newtonsoft.Json;
using NUnit.Framework;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Moq;
using System.Threading.Tasks;

namespace BotService.Test.Unit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("http://localhost:8081", "example")]
        public void Bot_should_work(string baseUrl, string token)
        {
            TelegramBotClient botClient = new(token, baseUrl: baseUrl);

            Assert.NotNull(botClient.BotId);
        }

        [TestCase("Фамилия: Иванов")]
        [TestCase("Имя: Иван")]
        [TestCase("Отчество: Иванович")]
        [TestCase("Должность: Директор")]
        [TestCase("ФИО Ген. Дир.: ИИИ")]
        [TestCase("Компания: ООО Ива")]
        public async Task Should_Send_Text_Message(string text, string baseUrl = "http://localhost:8081", string token = "example")
        {
            TelegramBotClient botClient = new (token, baseUrl: baseUrl);

            Message message = await botClient.SendTextMessageAsync(
                chatId: 123,
                text: text
            );

            Assert.AreEqual(text, message.Text);
            Assert.AreEqual(MessageType.Text, message.Type);
            Assert.AreEqual("123", message.Chat.Id.ToString());
        }
    }
}