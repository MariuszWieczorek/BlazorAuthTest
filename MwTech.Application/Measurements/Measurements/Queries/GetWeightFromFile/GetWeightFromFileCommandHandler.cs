using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Measurements.Measurements.Queries.GetWeightFromFile;

public class GetWeightFromFileCommandHandler : IRequestHandler<GetWeightFromFileCommand, string>
{
    private readonly IApplicationDbContext _context;

    public GetWeightFromFileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<string> Handle(GetWeightFromFileCommand request, CancellationToken cancellationToken)
    {

        //string path1 = @"\\kab-svr-fs02\users\walc\Moje dokumenty";
        //string path = @"\\kab-svr-fs02\users\kj.opony\Moje dokumenty";
        string path = @"\\kab-svr-fs02\office\!WYMIANA";
        string file = Path.Combine(path, "waga_odczyt.txt");
        decimal weight = 0;


        if (File.Exists(file))
        {
            //using (StreamReader reader = File.OpenText(file))

            var lastlines = File.ReadAllLines(file).Reverse().Take(1).Reverse();
            var x = lastlines.FirstOrDefault();
            x = x.Replace(" ", "");
            x = x.Replace(".", ",");

            decimal.TryParse(x, out weight);

        }

        return weight.ToString();
    }
}
