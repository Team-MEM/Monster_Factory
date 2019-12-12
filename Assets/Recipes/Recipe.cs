namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "", menuName = "Recipe")]
    public class Recipe : ScriptableObject
    {
        public string recipeName;
        public Sprite RecipeSprite;
        public int tier;
        public IngredientRecord[] ingredientRecords;
    }
}