using System.ComponentModel.DataAnnotations;

namespace AdminApp.Entities.Interfaces;

public interface ITracking
{
    [DisplayFormat(DataFormatString = "{yyyy-mm-dd}")]
    DateTime CreatedAt { get; set; }
    [DisplayFormat(DataFormatString = "{yyyy-mm-dd}")]
    DateTime? ModifiedAt { get; set; }
}