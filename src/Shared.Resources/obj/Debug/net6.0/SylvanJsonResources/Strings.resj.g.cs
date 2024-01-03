namespace Shared.Resources {

using global::System.Reflection;

/// <summary>
/// A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[global::System.Diagnostics.DebuggerNonUserCode()]
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Sylvan.BuildTools.Resources.JsonResourceGenerator", "0.6.1.0")]
public static partial class Strings
{
    static global::System.Resources.ResourceManager rm;
    static global::System.Globalization.CultureInfo resourceCulture;

    static global::System.Resources.ResourceManager ResourceManager
    {
        get
        {
            if (object.ReferenceEquals(rm, null))
            {
                rm = new global::System.Resources.ResourceManager("Strings", typeof(Strings).GetTypeInfo().Assembly);
            }

            return rm;
        }
    }

    static global::System.Globalization.CultureInfo Culture
    {
        get
        {
            return resourceCulture;
        }
        set
        {
            resourceCulture = value;
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Дополнительное примечание.
    /// </summary>
    public static string AdditionalNote
    {
        get
        {
            return ResourceManager.GetString("AdditionalNote", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Категория.
    /// </summary>
    public static string Category
    {
        get
        {
            return ResourceManager.GetString("Category", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Категория уже существует.
    /// </summary>
    public static string CategoryAlreadyExist
    {
        get
        {
            return ResourceManager.GetString("CategoryAlreadyExist", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Категория не найдена..
    /// </summary>
    public static string CategoryNotFound
    {
        get
        {
            return ResourceManager.GetString("CategoryNotFound", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Общее примечание.
    /// </summary>
    public static string CommonNote
    {
        get
        {
            return ResourceManager.GetString("CommonNote", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Создание прошло успешно..
    /// </summary>
    public static string CreateSuccessful
    {
        get
        {
            return ResourceManager.GetString("CreateSuccessful", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Удаление прошло успешно..
    /// </summary>
    public static string DeleteSuccessful
    {
        get
        {
            return ResourceManager.GetString("DeleteSuccessful", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Описание.
    /// </summary>
    public static string Description
    {
        get
        {
            return ResourceManager.GetString("Description", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Получение прошло успешно..
    /// </summary>
    public static string GetSuccessful
    {
        get
        {
            return ResourceManager.GetString("GetSuccessful", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Наименование.
    /// </summary>
    public static string Name
    {
        get
        {
            return ResourceManager.GetString("Name", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Стоимость.
    /// </summary>
    public static string Price
    {
        get
        {
            return ResourceManager.GetString("Price", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Продукт.
    /// </summary>
    public static string Product
    {
        get
        {
            return ResourceManager.GetString("Product", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Продукт уже существует..
    /// </summary>
    public static string ProductAlreadyExist
    {
        get
        {
            return ResourceManager.GetString("ProductAlreadyExist", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Продукт не найден..
    /// </summary>
    public static string ProductNotFound
    {
        get
        {
            return ResourceManager.GetString("ProductNotFound", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Поле "{PropertyName}" должно быть от {MinLength} до {MaxLength} символов..
    /// </summary>
    public static string PropertyMustBeCertainLength
    {
        get
        {
            return ResourceManager.GetString("PropertyMustBeCertainLength", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Поле "{PropertyName}" должно быть больше 0..
    /// </summary>
    public static string PropertyMustBeGreaterThanZero
    {
        get
        {
            return ResourceManager.GetString("PropertyMustBeGreaterThanZero", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Поле "{PropertyName}" не может быть пустым..
    /// </summary>
    public static string PropertyMustBeNotNull
    {
        get
        {
            return ResourceManager.GetString("PropertyMustBeNotNull", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Роль не найдена..
    /// </summary>
    public static string RoleNotFound
    {
        get
        {
            return ResourceManager.GetString("RoleNotFound", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Изменение прошло успешно..
    /// </summary>
    public static string UpdateSuccessful
    {
        get
        {
            return ResourceManager.GetString("UpdateSuccessful", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Пользователь уже существует..
    /// </summary>
    public static string UserAlreadyExist
    {
        get
        {
            return ResourceManager.GetString("UserAlreadyExist", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Пользователь уже имеет данные права..
    /// </summary>
    public static string UserAlreadyHaveRole
    {
        get
        {
            return ResourceManager.GetString("UserAlreadyHaveRole", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Пользователь уже имеет данные права..
    /// </summary>
    public static string UserAlreadyHaveNotRole
    {
        get
        {
            return ResourceManager.GetString("UserAlreadyHaveNotRole", resourceCulture);
        }
    }

    /// <summary>
    /// Looks up a localized string similar to Пользователь не найден..
    /// </summary>
    public static string UserNotFound
    {
        get
        {
            return ResourceManager.GetString("UserNotFound", resourceCulture);
        }
    }
}

}
