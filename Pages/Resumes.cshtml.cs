using aspnetWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetWebApp.Pages;

public class ResumesModel : PageModel
{
    public List<Student> Students { get; set; } = new();
    public List<string> Specialities { get; set; } = new();
    public string Search { get; set; } = "";
    public string SpecialityFilter { get; set; } = "";
    public void OnGet(string? search, string? specialityFilter)
    {
        // получаем все резюме
        var allStudents = CreateResumeModel.Students;
        // сохраняем значения из формы
        Search = search?.Trim() ?? "";
        SpecialityFilter = specialityFilter ?? "";
        // получаем список специальностей
        Specialities = allStudents
        .Select(student => student.Speciality)
        .Where(speciality => !string.IsNullOrWhiteSpace(speciality))
        .Distinct()
        .OrderBy(speciality => speciality)
        .ToList();
        // полный список резюме
        var filteredStudents = allStudents.AsEnumerable();
        // фильтруем по имени
        if (!string.IsNullOrWhiteSpace(Search))
        {
            filteredStudents = filteredStudents
            .Where(student => student.Name
            .Contains(Search, StringComparison.OrdinalIgnoreCase));
        }
        // фильтруем по специальности
        if (!string.IsNullOrWhiteSpace(SpecialityFilter))
        {
            filteredStudents = filteredStudents
            .Where(student => student.Speciality == specialityFilter);
        }
        Students = filteredStudents.ToList();
    }
}
