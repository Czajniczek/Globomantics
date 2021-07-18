namespace Shared.Models
{
    // Informacje o propozycji konferencji
    public class ProposalModel
    {
        public int Id { get; set; }

        public int ConferenceId { get; set; }

        public string Speaker { get; set; }

        public string Title { get; set; }

        public bool Approved { get; set; }
    }
}
