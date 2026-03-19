namespace DANGCAPNE.ViewModels
{
    public class ModulesIndexViewModel
    {
        public List<ModuleLinkViewModel> Modules { get; set; } = new();
    }

    public class ModuleLinkViewModel
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class DynamicCrudListViewModel
    {
        public string EntityKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Columns { get; set; } = new();
        public List<Dictionary<string, object?>> Rows { get; set; } = new();
    }

    public class DynamicCrudEditViewModel
    {
        public string EntityKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool IsEdit { get; set; }
        public List<DynamicField> Fields { get; set; } = new();
    }

    public class DynamicField
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Type { get; set; } = "text";
        public string? Value { get; set; }
        public bool IsKey { get; set; }
        public bool ReadOnly { get; set; }
        public List<SelectOption> Options { get; set; } = new();
    }

    public class SelectOption
    {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class DynamicCrudEditPostModel
    {
        public Dictionary<string, string?> Fields { get; set; } = new();
    }
}
