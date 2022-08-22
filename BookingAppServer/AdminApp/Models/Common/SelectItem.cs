namespace AdminApp.Models.Common;
public class SelectItem
{
   public string Id { get; set; } = string.Empty;
   public string Name { get; set; } = string.Empty;
   public bool Selected { get; set; }
   public object Select()
   {
      throw new NotImplementedException();
   }
}