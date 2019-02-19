using Roznamah.Model.Entities;
using Roznamah.Model.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roznamah.Services.Interfaces
{
    public interface IThemeService
    {
        IEnumerable<Theme> GetAllThemes();
        Theme GetTheme(int themeId);
    }
}
