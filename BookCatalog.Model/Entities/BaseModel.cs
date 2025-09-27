namespace BookCatalog.Model.Entities;

public abstract class BaseModel<T> where T : BaseModel<T>
{
    public DateTime? DataCriacao { get; set; }
    public DateTime DataModificacao { get; set; }

    public BaseModel()
    {
        if (DataCriacao is null)
            DataCriacao = DateTime.Now.ToLocalTime();

        DataModificacao = DateTime.Now.ToLocalTime();
    }
}
