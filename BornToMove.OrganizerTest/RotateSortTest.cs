namespace BornToMove.OrganizerTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test(Description = "Rotate sort empty list")]
    public void TestSortEmpty()
    {
        // prepare
        RotateSort<int> rotateSorter = new RotateSort<int>();
        IComparer<int> comparer = new Comparer();
        List<int> input = new List<int>() { };
        // run
        var result = rotateSorter?.Sort(input, comparer);
        // validate
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(0).Items);
        Assert.That(result, Is.EquivalentTo(new List<int> { }));

    }

    [Test(Description = "Rotate sort a list with 1 integer")]
    public void TestSortOneElements()
    {
        // prepare
        RotateSort<int> rotateSorter = new RotateSort<int>();
        IComparer<int> comparer = new Comparer();
        List<int> input = new List<int>() { 5 };
        // run
        var result = rotateSorter?.Sort(input, comparer);
        // validate
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result, Is.EquivalentTo(new List<int> { 5 }));
    }

    [Test(Description = "Rotate sort a list with 2 integers")]
    public void TestSortTwoElements()
    {
        // prepare
        RotateSort<int> rotateSorter = new RotateSort<int>();
        IComparer<int> comparer = new Comparer();
        List<int> input = new List<int>() { 32, 3 };
        // run
        var result = rotateSorter?.Sort(input, comparer);
        // validate
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(2).Items);
        Assert.That(result, Is.EquivalentTo(new List<int> { 3, 32 }));
        Assert.That(input, Is.EquivalentTo(new List<int> { 32, 3 }));
    }
}