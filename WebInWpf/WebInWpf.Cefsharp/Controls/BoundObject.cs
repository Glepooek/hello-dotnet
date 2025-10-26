using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebInWpf.Cefsharp.Controls;

public class BoundObject
{
    public void onMessageReceived(string message)
    {
        Console.WriteLine(message);
    }

    public string getUserInfo()
    {
        return JsonSerializer.Serialize(new
        {
            name = "unipus",
            age = 14
        });
    }
}
