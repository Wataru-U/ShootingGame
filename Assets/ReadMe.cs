/*

    参照URL
        https://qiita.com/okuhiiro/items/4c76fd8862e2bbb08ac7　１秒ごと
        アニメーしょん
        https://qiita.com/nskydiving/items/c9c47c1e48ea365f8995 linq
        https://www.sejuku.net/blog/55029
        http://kurihara-n.hatenablog.com/entry/2016/08/13/011728
        https://freesworder.net/unity-scene-change/   シーン
        https://freesworder.net/unity-variable-unchange/　シーン変数
        https://qiita.com/haifuri/items/0a03270b1b3d4331196b　//si-nnmei
        https://qiita.com/WassyPG/items/533785b5e36967a8f2a5　canvasこてい
        https://tech.pjin.jp/blog/2017/03/22/unity_ugui_rect-transform/ てxt固定位
        https://qiita.com/nonkapibara/items/712a14c384b5ba5a9f7a フォンtz￥しぜ
        https://teratail.com/questions/235928 font screenに対して
        https://gamefbb.com/%E3%80%90unity%E3%80%91gameobject%E3%81%AE%E5%90%8D%E5%89%8D%E3%82%92%E5%8F%96%E5%BE%97%E3%83%BB%E5%A4%89%E6%9B%B4%E3%81%99%E3%82%8B/  名前取得
        https://vend9520-lab.net/?p=382
        https://qiita.com/OKsaiyowa/items/f995ad9c0884fb2ced8f  点滅
*/


/*   /// 使うとなんかのフォルダができる？よくわかってない
</sumamry>
//  コントローラー操作　DualShock4以外は知らない
/       左スティック　移動
///     L2　　 　　弾
///     右スティック　Dirの変更
///     R1        Dirが代わる速度をあげる
///     R2        回転する弾の時、回る向きが逆になる
///     
///
/// 操作 ・・・ 矢印　sapce
//        　Dir   z c 
//         ターゲット変更　a s
/// 敵
///     一定ダメージ　・・・　カウント＋＋　再出現
///     画面外  　  ・・・　再出現
</sumamry>
*/

/*
    player 
        TODO 弾の選択(番号に合わせてinstantiate(objNumber))
        TODO GUIの実装
        TODO ターゲットの固定

    bullett 
        TODO BulletManaggerを作って打つ球の管理
        TODO 曲がる球が曲がらなくなってるからDTDPの計算
        TODO 弾の威力の実装
        TODO ホーミング弾の向きを直す

    enemy 
        TODO エネミーの 軌道 弾　を考える

　　その他     
　　     TODO コードを綺麗にする
        見た目
        敵は動かないほうがいい？
*/


/*
    ログ
        球の製作
            position += DtimeDposition
            まっすぐ
            曲がる
            sin波
            回りながら　
            回転する　＝＞　スタートからの時間に合わせたため時間が経ってから打つと半径がでかい

        プレイヤー　と　弾の発射
            問題
                まっすぐが判定とずれてる
                弾の角度が決められない
                回転する弾が使えない
        
        エネミーの製作
            プレイヤーに向かって
            hpが尽きると次が出現　＝＞　カウント＋＋；
            外に出ると再出現　＝＞　カウント　＋＝　０；

        コントローラーの設定
            プレイヤーを動かす
            球を出す
            dirの設定　＝＞　球毎のDirの廃止　＝＞　曲がる球が曲がらない
            回転する弾の修正
            
        残りHPに応じて色を変える
            プレイヤーもエネミーも同じ書き方(コピペ)

        敵を増やした(仮)
            動かないものを追加
            動かないものを倒すと動くのが出る
            全部出るわけではない　あとで直す

        ホーミング弾の作成
            優先度付きキューで近さをソートする
            playerManaggerで近さに応じたtargetの設定
            targetに向かって移動
            targetがデストロイされた時弾が困る
            targetの指標の変更

        見た目とか
            ハチとかを作った
            playerが横に動いてた状態から縦に動くよう仕様の変更(球は横方向に変更)
            ↑90ど向きを変えた
            エネミーのHPが0になったら信号を出してホーミング弾を消してから壊しすことでエラーを直した
            蝶を定期的、倒したら再出現させたら増えすぎた
*/