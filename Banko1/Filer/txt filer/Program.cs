using System;

public class Class1{
	public Class1(){

        Random rnd = new Random();
        int Value = rnd.Next(1, 10);

        Console.WriteLine(Value);
        Console.ReadLine();
    }
}
