using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Settings")]
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private float damage;
    [SerializeField] private WeaponElement weaponElement;
    [SerializeField] private int masteryLevel;
    [SerializeField] private WeaponLevel weaponLevel;
}
public enum WeaponType
{
    Hand,
    Sword,
    Axe,
    Bow,
    Dagger,
    Spear,
    Fan,
}
public enum WeaponLevel
{
    LinhBao,
    TienBao,
    ThanhBao,
    ThanBao,
    ThienBao
}
public enum WeaponElement
{
    Physic,
    Fire,
    Ice,
    Thunder,
    Wind,
    Light,
    Dark,
    Love,
    Lust,
    Life,
    Death,
    Time,
    Space
}
