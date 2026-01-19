namespace Squares.Application.DTOs
{
    public record PointDto(int x, int y);
    public record SquareResponseDto(int TotalCount, List<List<PointDto>> Squares);
    public record CreateListRequest(string Name, List<PointDto> Points);
}
