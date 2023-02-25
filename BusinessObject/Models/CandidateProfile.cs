using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public partial class CandidateProfile
    {
        [Required(ErrorMessage = "This field is Required!")]
        public string CandidateId { get; set; } = null!;

        [Required(ErrorMessage = "This field is Required!")]
        [MinLength(12, ErrorMessage = "Full name must be greater than 12 characters")]
        [FirstLetterCapitalize(ErrorMessage = "Full name must have first character capitalize")]
        public string Fullname { get; set; } = null!;
        [Required(ErrorMessage = "This field is Required!")]
        public DateTime? Birthday { get; set; }
        [Required(ErrorMessage = "This field is Required!")]
        [MinLength(12, ErrorMessage = "Description Must be larger than 12 characters")]
        [MaxLength(200, ErrorMessage = "Description cannot be over 200 characters")]
        public string? ProfileShortDescription { get; set; }
        [Required(ErrorMessage = "This field is Required!")]
        public string? ProfileUrl { get; set; }
        public string? PostingId { get; set; }

        public virtual JobPosting? Posting { get; set; }
    }
}
