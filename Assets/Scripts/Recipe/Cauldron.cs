using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cam.CamController;
using Inputs;
using Select;
using System.Linq;
public enum ItemType
{
    GlowStoneDust,
    Water,
    Spider,
    Leaf
}
public class Cauldron : Singleton<Cauldron>
{
    public delegate void ClearCauldron();
    public ClearCauldron clearCauldron;
    [SerializeField] private GameObject _trashButton;
    [SerializeField] private GameObject _closeImage,_touchImage;
    private float angle = 0;
    private Vector3 _spoonStartPos;
    private Bottle bottle;
    [SerializeField] private GameObject _spoon;
    [SerializeField] private InputData _inputData;
    [SerializeField] private float _minY, _currentY,_radius,_distanceFactor,_speed,_maxMix;
    [SerializeField] private Vector3 oldPos,oldRot;
    [SerializeField] private Transform _cauldronZoomTransform;
    private float _totalY;
    private List<GameObject> recipeItems = new List<GameObject>();
    public List<RecipeList> allRecipes;
    public List<RecipeList> useableRecipes;
    public List<Recipe> currentRecipe;
    public bool isSelected,isMixed;
    private const string RECIPE_KEY = "Recipe";
    void OnEnable()
    {
        // for (int i = 0; i < allRecipes.Count; i++)
        // {
        //     if(PlayerPrefs.GetInt(RECIPE_KEY) == 1)
        //     {
        //         allRecipes[i].Unlock();
        //         allRecipes[i].createdObj.SetActive(true);
        //     }
        // }
        StageManager.Instance.onSwipeRight += ToggleTrashButton;
        StageManager.Instance.onSwipeLeft += ToggleTrashButton;
    }
    void OnDisable()
    {
        // for (int i = 0; i < allRecipes.Count; i++)
        // {
        //     Debug.Log(allRecipes[i]+ " allRecipes[i])");
        //     Debug.Log(allRecipes[i].bookRecipe + " allRecipes[i]).bookrecipe");
        //     if(allRecipes[i].bookRecipe.activeSelf)
        //     {
        //         PlayerPrefs.SetInt(RECIPE_KEY,1);
        //     }
        //     else
        //         PlayerPrefs.SetInt(RECIPE_KEY,0);
        // }
        StageManager.Instance.onSwipeRight -= ToggleTrashButton;
        StageManager.Instance.onSwipeLeft -= ToggleTrashButton;
        
    }
    void Start()
    {
        bottle = GetComponentInChildren<Bottle>();
        _spoonStartPos = _spoon.transform.position;
    }
    void Update()
    {
        Mix();
    }
    public void AddRecipeItem(GameObject item)
    {
        recipeItems.Add(item);
    }
    public bool CheckWater()
    {
        return bottle.GetCurrentColorFull();
    }
    private void CheckRecipe()
    {
        foreach (var item in useableRecipes)
        {
            if(CompareRecipe(item))
            {
                if(isMixed)
                    StartCoroutine(CameraStageAnim(item.createdObj));
            }
        }
    }
   
    IEnumerator CameraStageAnim(GameObject obj)
    {
        StageManager.Instance.ToggleArrows(false);
        // int currentStage = StageManager.Instance.index;
        SelectManager.Instance.isAnimation = true;
        StageManager.Instance.ForceRight();
        yield return new WaitForSeconds(.5f);
        StageManager.Instance.ForceRight();
        yield return new WaitForSeconds(1f);
        obj.SetActive(true);
        Outline outline = obj.AddComponent<Outline>();
        yield return new WaitForSeconds(1f);
        StageManager.Instance.ForceLeft();
        yield return new WaitForSeconds(.5f);
        StageManager.Instance.ForceLeft();
        SelectManager.Instance.isAnimation = false;
        StageManager.Instance.ToggleArrows(true);
        Destroy(outline);
    }
    private bool CompareRecipe(RecipeList recipeList)
    {
        bool qwe = false;
        foreach (var item in recipeList.recipe)
        {
            foreach (var _recipe in currentRecipe)
            {
                if(item.type == _recipe.type && item.count == _recipe.count)
                {       
                    qwe = true;
                }
            }
            if(!qwe) return false;
            qwe = false;
        }
        return true;
    }
    public void AddRecipe(ItemType itemType)
    {
        foreach (var item in currentRecipe)
        {
            if(itemType == item.type)
            {
                item.count ++;
                CheckRecipe();
                return;
            }
        }
        currentRecipe.Add(new Recipe(itemType,1));
        CheckRecipe();
    }
    public void AddAllRecipes(RecipeList recipeList)
    {
        useableRecipes.Add(recipeList);
    }
    public void EmptyCauldron()
    {
        foreach (var item in recipeItems)
        {
            Destroy(item);
        }
        ItemTypeHolder.Instance.ClearItems();
        isMixed = false;
        _totalY = 0;
        currentRecipe.Clear();
        bottle.ResetBottle();
        clearCauldron?.Invoke();
    }

    public void MoveCamera()
    {
        if(isSelected) return;
        if(ShakerMovement.Instance.IsCurrentShaker()) return;
        _touchImage.SetActive(true);
        _closeImage.SetActive(true);
        isSelected = true;
        oldPos = CameraController.Instance.transform.position;
        oldRot = CameraController.Instance.transform.rotation.eulerAngles;
        CameraController.Instance.PosAndRotate(_cauldronZoomTransform.position,_cauldronZoomTransform.rotation.eulerAngles,.3f);
    }
    private void Mix()
    {
        if(!isSelected) return;
        if(_inputData.IsMove)
        {
            _currentY += Mathf.Abs (_inputData.DeltaPosition.y);
            
        }
        if(_inputData.IsEnd)
            _currentY = 0;
        if(_currentY > _minY)
        {
            angle += _speed;
        }
        _totalY = _currentY;
        if(_totalY > _maxMix)
        {
            isMixed = true;
        }
        _spoon.transform.position =   AngleTo(angle);
    }
    public void Close()
    {
        _touchImage.SetActive(false);
        _closeImage.SetActive(false);
        isSelected = false;
        CameraController.Instance.PosAndRotate(oldPos,oldRot,.3f,()=>CheckRecipe());
    }
    private Vector3 AngleTo(float angle)
    {
        float x = _spoonStartPos.x + _radius * Mathf.Cos(angle*Mathf.Deg2Rad) ;
        float z =_spoonStartPos.z + _radius * Mathf.Sin(angle*Mathf.Deg2Rad) ;
        return new Vector3(x, _spoon.transform.position.y, z);
    }
    public bool CheckShake()
    {
        if(isSelected) return false;
        if(!CheckWater()) return false;
        return true;
    }
    private void ToggleTrashButton()
    {
        if(StageManager.Instance.index == StageManager.CAULDRONINDEX)
        {
            _trashButton.SetActive(true);
        }
        else
            _trashButton.SetActive(false);
    }
    public RecipeList GetRecipeList(ColorType colorType)
    {
        foreach (var item in allRecipes)
        {
            if(item.colorType == colorType)
            {
                return item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class Recipe
{
    public ItemType type;
    public int count;
    
    public Recipe(ItemType itemType,int count)
    {
        this.type = itemType;
        this.count = count;
    }
    
}
[System.Serializable]
public class RecipeList
{
    public ColorType colorType;
    public List<Recipe> recipe;
    public GameObject createdObj;
    public GameObject bookRecipe;
    public GameObject questionMarks;
    public void Unlock()
    {
        if(questionMarks!=null)
            questionMarks.SetActive(false);
        bookRecipe.SetActive(true);
    }
}
    
