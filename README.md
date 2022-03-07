# **----------projectAPI2D----------** 
> ### 注意事項 : 使用時請直接繼承 projectAPI2D ，需要使用 Awake 時請在 LocalAwake 內編碼。
> (否則會覆寫掉專案自帶的 Awake)

## **【緩存物件】**
> #### 若腳本所掛載的物件中包含下列組件，可直接調用，不需自行 GetComponent<>  (小寫開頭)
> ![image](https://github.com/YsNaive/NaiveUnityPack2D/blob/e2b7a30700e1cbe31cfb3adeec8b056913ffc071/Documentation~/icon/animator.png)  
> ![image](https://github.com/YsNaive/NaiveUnityPack2D/blob/e2b7a30700e1cbe31cfb3adeec8b056913ffc071/Documentation~/icon/spriteRenderer.png)


## **【取值函式】**
> ### Vector2 mousePos()  
>> #### 取得使用者滑鼠在坐標系上的位置  

> ### GameObject myParent()
>> #### 取得自身父物件

> ### GameObject[] myChild( 可選參數1 , 可選參數2)
>> #### 取得自身子物件  
>>> - 無輸入  
>>> 回傳所有子物件  
>>> - 可選參數1  
>>> 輸入 Tag(string) 進行匹配搜尋  
>>> 輸入 int 回傳指定順序之物件 (此時輸出 GameObject 而非陣列)   
>>> - 可選參數2  
>>> 輸入 True 將只會回傳第一個匹配到的物件 (此時輸出 GameObject 而非陣列)  


## **【數學函式】**
> ### float obj2angle2D(必填參數1 , 必填參數2)
>> #### 回傳兩參數之間的角度(以參數1為原點)
>>> 輸入可使用 GameObject 或是 Vector2


## **【快捷參數】**
> ### void setAnimator()
>> #### 設定此物件之 Animator 參數，輸入格式與 Unity 預設相同，但會自動判斷型別，不須指定
>>> 觸發 trigger 使用 setAnimator("name")  
>>> 設定 bool 則用 setAnimator("name", true) 

> ### void setColor()
>> #### 設定此物件之 Color 參數，可直接輸入Color型別或是下列格式(皆為 float)
>>> setColor(A) 僅調整透明度  
>>> setColor(R,G,B) 僅調整 RGB
>>> setColor(R,G,B,A) 調整全部


# **----------素材包使用建議----------**  
> - 使用 Tilemap 相關資源時可直接開啟調色板使用不需進行變動，隨專案更新即可【需覆寫更新】。  
> - 使用 Dream Tale 中角色、怪物時自行建立 Animator 後再調用動畫，預設 Animator 為參考用  
> !!! **不要將自訂腳本放在 Sample 與其任何子資料夾，更新時有被覆寫的風險** !!!  
## **【Tilemap】**
### 注意事項：本素材建構於套件 2D Tilemap Extras 之上  
https://github.com/Unity-Technologies/2d-extras  

> - **Floor**  
> stoneBrick - 可全角度使用   
> ![image](https://github.com/YsNaive/NaiveUnityPack2D/blob/e2b7a30700e1cbe31cfb3adeec8b056913ffc071/Documentation~/floor/floor_stoneBrick.png)
> - **Wall**  
> chineseWall_1 - 僅支援直角、直線  
> ![image](https://github.com/YsNaive/NaiveUnityPack2D/blob/e2b7a30700e1cbe31cfb3adeec8b056913ffc071/Documentation~/wall/wall_onlyBlock.png)

## **【Dream Story】**
> - **Little Red Riding Hood**  
> #### 包含 Sample Animator
> ![image](https://github.com/YsNaive/NaiveUnityPack2D/blob/e2b7a30700e1cbe31cfb3adeec8b056913ffc071/Documentation~/monster/stage1%20sample.png)
