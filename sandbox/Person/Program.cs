using System.Dynamic;

class Program
{
    static void GetPersonInformation(Person person)
    {
        Console.WriteLine($"{person.GetPersonInformation}");
    }
    static void Main()
    {
        Person myPerson = new Person("Bob", "Floyd", 64, 195);
        myPerson.GetPersonInformation();
        Police myPolice = new Police("Lance", "Bob", 34, 250, "P90");
        myPolice.GetPersonInformation();

        List<Person> myPeople = new List<Person>();

        myPeople.Add(myPerson);
        myPeople.Add(myPolice);


        foreach(Person person in myPeople)
        {
            GetPersonInformation(person);
        }
    }
}