using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    //create enum for the piece types available
    public enum Piece
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    [Header("Piece Options")]
    public Piece piece;

    public Color pieceColor = Color.white;

    [Header("Sprites")]
    public Sprite pawnSprite;
    public Sprite knightSprite;
    public Sprite bishopSprite;
    public Sprite rookSprite;
    public Sprite queenSprite;
    public Sprite kingSprite;

    private SpriteRenderer spriteRenderer;

    //method to create sprite renderer
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePiece();
    }

    //method to run when the type of piece is changed in the editor
    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePiece();
    }

    //method to change the sprite after the selection is made
    private void UpdatePiece()
    {
        if (spriteRenderer == null)
            return;

        spriteRenderer.color = pieceColor;

        switch (piece)
        {
            case Piece.Pawn:
                spriteRenderer.sprite = pawnSprite;
                break;

            case Piece.Knight:
                spriteRenderer.sprite = knightSprite;
                break;

            case Piece.Bishop:
                spriteRenderer.sprite = bishopSprite;
                break;

            case Piece.Rook:
                spriteRenderer.sprite = rookSprite;
                break;

            case Piece.Queen:
                spriteRenderer.sprite = queenSprite;
                break;

            case Piece.King:
                spriteRenderer.sprite = kingSprite;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //set the color of the movement indicator
        Gizmos.color = Color.yellow;

        //decide which piece type is selected and give appropriate movement allowances (arbitrary full board length for each movement - can be replaced with boardsize and edge logic)
        switch (piece)
        {
            case Piece.Pawn:
                for (int i = 0; i < 8; i++)
                {
                    Gizmos.DrawCube((transform.position + new Vector3(0, i * 1f, 0)), new Vector3(1f, 1f, 0));
                }
                break;

            case Piece.Knight:
                Gizmos.DrawCube((transform.position + new Vector3(2f,1f,0)), new Vector3(1f,1f,0));
                Gizmos.DrawCube((transform.position + new Vector3(2f, -1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-2f, 1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-2f, -1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(1f, 2f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(1f, -2f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-1f, 2f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-1f, -2f, 0)), new Vector3(1f, 1f, 0));
                break;

            case Piece.Bishop:
                for(int i = 0; i < 8; i++)
                {
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, i * 1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, i * -1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, i * -1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, i * 1f, 0)), new Vector3(1f, 1f, 0));
                }
                break;

            case Piece.Rook:
                for (int i = 0; i < 8; i++)
                {
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, 0, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, 0, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(0, i * 1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(0, i * -1f, 0)), new Vector3(1f, 1f, 0));
                }
                break;

            case Piece.Queen:
                for (int i = 0; i < 8; i++)
                {
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, i * 1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, i * -1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, i * -1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, i * 1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * 1f, 0, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(i * -1f, 0, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(0, i * 1f, 0)), new Vector3(1f, 1f, 0));
                    Gizmos.DrawCube((transform.position + new Vector3(0, i * -1f, 0)), new Vector3(1f, 1f, 0));
                }
                break;

            case Piece.King:
                Gizmos.DrawCube((transform.position + new Vector3(1f, 1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-1f, -1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(1f, -1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-1f, 1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(1f, 0, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(-1f, 0, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(0, 1f, 0)), new Vector3(1f, 1f, 0));
                Gizmos.DrawCube((transform.position + new Vector3(0, -1f, 0)), new Vector3(1f, 1f, 0));
                break;
        }
    }
}
