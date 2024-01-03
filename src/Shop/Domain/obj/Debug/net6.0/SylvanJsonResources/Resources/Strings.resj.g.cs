namespace Shop.Domain.Resources {

using global::System.Reflection;

/// <summary>
/// A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[global::System.Diagnostics.DebuggerNonUserCode()]
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Sylvan.BuildTools.Resources.JsonResourceGenerator", "0.6.1.0")]
static partial class Strings
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
    /// Looks up a localized string similar to Не известно.
    /// </summary>
    public static string UNKNOWN
    {
        get
        {
            return ResourceManager.GetString("UNKNOWN", resourceCulture);
        }
    }
}

}
