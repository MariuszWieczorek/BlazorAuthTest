using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Measurements.Measurements.Queries.GetWeightFromFile;

public class GetWeightFromFileCommand : IRequest<string>
{

}
