using AllTheBeans.Models;

namespace AllTheBeans.UnitTests;

public abstract class BeanTestBase
{
    protected readonly List<Bean> _beans =
    [
        new()
        {
            Id = "66a374596122a40616cb8599",
            IsBOTD = false,
            CostInGBP = 39.26,
            ImageUrl = "https://images.unsplash.com/photo-1672306319681-7b6d7ef349cf",
            Name = "TURNABOUT",
            Description =
                "Ipsum cupidatat nisi do elit veniam Lorem magna. Ullamco qui exercitation fugiat pariatur sunt dolore Lorem magna magna pariatur minim. Officia amet incididunt ad proident. Dolore est irure ex fugiat. Voluptate sunt qui ut irure commodo excepteur enim labore incididunt quis duis. Velit anim amet tempor ut labore sint deserunt.\r\n",
            CountryId = 1,
            ColourId = 1,
        },
        new()
        {
            Id = "66a374591a995a2b48761408",
            IsBOTD = false,
            CostInGBP = 18.57,
            ImageUrl = "https://images.unsplash.com/photo-1641399756770-9b0b104e67c1",
            Name = "ISONUS",
            Description =
                "Dolor fugiat duis dolore ut occaecat. Excepteur nostrud velit aute dolore sint labore do eu amet. Anim adipisicing quis ut excepteur tempor magna reprehenderit non ut excepteur minim. Anim dolore eiusmod nisi nulla aliquip aliqua occaecat.\r\n",
            CountryId = 2,
            ColourId = 2,
        }
    ];
}