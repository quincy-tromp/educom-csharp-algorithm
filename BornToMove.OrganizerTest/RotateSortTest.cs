using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest;

public class RotateSortTest
{
    [Test]
    public void TestSortEmpty()
    {
        // prepare
        RotateSort<int> rotateSorter = new RotateSort<int>();
        List<int> input = new List<int>() { };
        IComparer<int> comparer = new Comparer();
        // run
        var result = rotateSorter.Sort(input, comparer);
        // validate
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Exactly(0).Items);
        Assert.That(result, Is.EquivalentTo(new List<int> { }));
    }
}

