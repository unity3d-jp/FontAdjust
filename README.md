# FontAdjust について
Unity5.3系→5.4等に移行すると、UguiのTextが Y方向にずれてしまう問題があります。<br />
![alt text](doc/img/53xto54x.png)

5.3ベースで作成していただいた方々が、5.4以降にスムーズに移行する手助けのため、本ツールを用意させていただきました。<br />

下記のような形で動作いたします<br />
![alt text](doc/img/FontAdjust.gif)  


※1.フォントのデータ次第で、ずれ方が違っておりましたため 全てカバーしきれるかはわかりません。<br />
※2.また複数行ある場合などでの動作も保証は出来ない状態になっております。<br />

# 使用方法等

FontAdjust.unitypackage をプロジェクトにインポートすることで必要なものが入ります。<br />

メニューの Tools/FontAdjust/FontAdjustWindowを呼び出すと下記ウィンドウが出てきます。<br />
![alt text](doc/img/FontAdjustWindow.png)  

・Modeの項目では、上にずらすか下にずらすかを選択できます。5.3->5.4ならば「position up」に指定してください。<br />
・「Execute each prefab」では、Project内にあります全てのprefab中の全てのUI.Textに対して処理をします。<br />
・「Execute All Scene」では、Project内にあります全てのシーンの全てのUI.Textに対して処理をします。<br />
・「Execute Current Scene」では、現在開いているシーンの全てのUI.Textに対して処理をします。<br />



# サンプルについて
動作の確認ができるためにサンプルを用意いたしました。<br />
testシーンを開いたうえで、「Tools/FontAdujust/Debug/CreateTest」にて下記ウインドウを呼び出してください<br />
![alt text](doc/img/CreateTest.png)  

GameObjectを指定する際には、プロジェクト内にあります以下prefabのいづれかを指定してください。<br />
 - testTemplateBottom<br/>
 - testTemplateMiddle<br/>
 - testTemplateTop<br/>
 - testTemplateChildObject<br/>

その後「CreateTest」ボタンを押しますと、プロジェクト内にあるFontデータ分だけテスト用にボタンオブジェクトを生成します。<br />
それが完了したら、「AdjustTo53」ないしは「AdjustFrom53」を押すことでフォント位置が変わることを確認できると思います。

# 同梱しているフォントについて
こちらでは、m+フォントを利用させていただいております。<br />
https://mplus-fonts.osdn.jp/

また デモをわかりやすくするために、FontForgeというツールを利用して改変したフォントを同梱しております。

