
namespace OnlineBookStore.Common.Application;

public abstract class ApplicationMessages
{
    public const string DefaultSucess = "عملیات با موفقیت انجام شد";
    public const string DefaultFailer = "عملیات با  شکست مواجه شد";
    public const string NotFoundError = "دیتای مورد نظر یافت نشد";
    public const string NotSavedError = "ذخیره سازی با شکست مواجه شد دوباره سعی کنید";
    public const string DefaultExceptionError = "خظای در نرم افزار رخ داده است";

    public static string ExceptionMessage(string methodName, string entityName) => $"an error ouccer in the {methodName} and entity {entityName}";
}
