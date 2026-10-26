class Program
{
    static void Main()
    {
        Person myPerson = new Person("Bob", "Floyd", 64, 195);
        myPerson.DisplayPersonInformation();
        Police myPolice = new Police("Lance", "Bob", 34, 250, "P90");
        myPolice.DisplayPersonInformation();
    }
}