using System.Security.Claims;

namespace LearnCraftt.Api.Extensions;

//controllerda, JWT icindeki UserId'yi temiz ve tekrar kullanilabilir sekilde almak icin
public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        // JWT icindeki UserId'yi temsil eden ilk claim bulunur
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            throw new UnauthorizedAccessException("UserId claim not found.");

        return Guid.Parse(userIdClaim.Value);} // string gelen claim'i guid'e cevirir.
}


/* extension method, var olan bir sinifa, kaynak kodunu degistirmeden yeni bir metot
 eklememizi saglar. Extension method olmasa her seferinde tekrar tekrar ihoyac olan yerlerde yazmamiz gerekecek
 Extension olmadan 
 var userId = Guid.Parse(
   HttpContext.User.Claims
       .First(x => x.Type == ClaimTypes.NameIdentifier)
       .Value);
       
Extension varken
var userId = User.GetUserId();
WT’den gelen kullanıcı kimliğini controller’da tekrar tekrar parse etmemek için 
ClaimsPrincipal’a extension method yazdım. Böylece hem okunabilirliği artırdım hem de authentication logic’i tek yerde topladım.”
ClaimsPrincipal, sisteme giriş yapmış kullanıcının kim olduğunu ve hangi bilgilere (claim’lere) sahip olduğunu temsil eden .NET nesnesidir.
*/