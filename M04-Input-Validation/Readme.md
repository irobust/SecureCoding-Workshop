# Input Validation
### Whitelisting
```
public class PersonModel
{
    ...

    [Required]
    [RegularExpression(@"^1?\d{0,2}$")]
    public string Age { get; set; }

    ...
}
```

### Custom Input Validation
1. Extend Regular Expression attribute
```
public class LimitAttribute: RegularExpressionAttribute
{
    public LimitAttribute(): base(@"^\d{2}$")
    {
        ErrorMessage = "Limit must be 10-99";
    }
}
```

2. Custom Validator
```
public class FileName: ValidationAttribute
{
    public FileName()
    {
        ErrorMessage = "File Name must be jpg or png";
    }

    public override bool IsValid(object value)
    {
        var extensions = new[] { ".jpeg", ".jpg", ".png" };
        bool result = extensions.Any(ext => value.ToString().Contains(ext));
        return base.IsValid(result);
    }
}
```

