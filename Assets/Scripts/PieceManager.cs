using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject controller;

    private Game game;

    // our images
    public Sprite hidden_empty;
    public Sprite hidden_ship;
    public Sprite hit_ship;
    public Sprite miss_empty;

    // our sounds
    public AudioSource hitSound;
    public AudioSource missSound;

    private int xBoard = -1;
    private int yBoard = -1;

    private void Start()
    {
        // Get the parent script engine thingy so we can tell it when the user hits something
        game = FindObjectOfType<Game>();
    }

    public void Activate()
    {
        // Setup
        controller = GameObject.FindGameObjectWithTag("GameController");

        missSound = GameObject.Find("MissSound").GetComponent<AudioSource>();
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();

        SetCoords();

        // Set the sprite image depending on the pieces name
        switch (this.name)
        {
            case "hidden_empty":
                this.GetComponent<SpriteRenderer>().sprite = hidden_empty;
                break;
            case "hidden_ship":
                this.GetComponent<SpriteRenderer>().sprite = hidden_ship;
                break;
            case "hit_ship":
                this.GetComponent<SpriteRenderer>().sprite = hit_ship;
                break;
            case "miss_empty":
                this.GetComponent<SpriteRenderer>().sprite = miss_empty;
                break;
        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

    public void PlayMissSound()
    {
        missSound.Play();
    }

    void Update()
    {

        // did they click on the screen?
        if (Input.GetMouseButtonDown(0)) 
        {
            // draw a magic line from where they clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // apparently figure out what they clicked on
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // check if its a sprite they clicked or something else
            if (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>() != null)
            {
                SpriteRenderer spriteHit = hit.collider.GetComponent<SpriteRenderer>();

                // If they clicked on a hidden piece, change it to a miss or hit
                if (spriteHit.name == "hidden_empty")
                {
                    spriteHit.sprite = miss_empty;
                    spriteHit.name = "miss_empty";

                    PlayMissSound();
                }
                if (spriteHit.name == "hidden_ship")
                {
                    spriteHit.sprite = hit_ship;
                    spriteHit.name = "hit_ship";

                    PlayHitSound();                    

                    game.IsItGameOver();
                }
            }
        }
    }
}