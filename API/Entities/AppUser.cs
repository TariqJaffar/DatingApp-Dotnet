namespace API.Entities;

public class AppUser
{
    
public int Id { get; set; }
public required string UserName { get; set; }
 public  byte[] PasswordHash { get; set; }=[];
 public  byte[] PasswordSalt { get; set; }=[];
public DateOnly DateOfBirth { get; set; }
public required string KnownAs { get; set; }
public DateTime Created {set;get;}=DateTime.UtcNow;
public DateTime LastActive { get; set; }
public required string Gender{set;get;}
public string? Introduction { get; set; }
public string? Interests { get; set; }
public string? LookingFor{get;set;}
public required string City { get; set; }
public required string Country { get; set; }
public List<Photo> photos{set;get;}=[];


public int GetAge(){
    return DateOfBirth.CalculateAge();
}





}
