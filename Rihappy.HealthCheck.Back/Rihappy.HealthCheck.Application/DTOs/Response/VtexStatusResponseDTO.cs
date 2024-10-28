using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.DTOs.Response
{
public class VtexStatusResponseDTO
{
    public string CategoryName { get; set; }
    public List<CategoryComponentsDto> Components { get; set; }
}

public class CategoryComponentsDto
{
    public string GroupName { get; set; }
    public List<ComponentDto> Components { get; set; }
}

public class ComponentDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}


}
