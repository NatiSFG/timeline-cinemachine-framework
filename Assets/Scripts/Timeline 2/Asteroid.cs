using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField] private int rotSpeed;

    private Vector3 vect;

    private void Start() {
        rotSpeed = Random.Range(0, 50);
        int randX = Random.Range(-360, 360);
        int randY = Random.Range(-360, 360);
        int randZ = Random.Range(-360, 360);
        vect = new Vector3(randX, randY, randZ);
    }

    private void Update() {
        float Rotation = rotSpeed * Time.deltaTime;
        transform.Rotate(vect, Rotation);
    }
}