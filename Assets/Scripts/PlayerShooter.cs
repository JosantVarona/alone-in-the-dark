using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _gunOffset;
    [SerializeField] private float _timeBetweenShots = 0.5f; 

    private float _nextShotTime; 

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _nextShotTime)
        {
            FireBullet();
            _nextShotTime = Time.time + _timeBetweenShots; 
        }
    }

    private void FireBullet()
    {
        // Obtén la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Asegura que el z sea 0, ya que estamos en 2D

        // Calcula la dirección entre el jugador y el ratón
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Instancia la bala desde la posición del arma o jugador
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, Quaternion.identity);

        // Aplica velocidad en la dirección hacia el ratón
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * _bulletSpeed;
    }
}
