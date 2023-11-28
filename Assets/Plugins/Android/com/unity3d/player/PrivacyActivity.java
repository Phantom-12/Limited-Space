package com.unity3d.player;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
 
public class PrivacyActivity extends Activity implements DialogInterface.OnClickListener {
    boolean useLocalHtml = true;
    String privacyUrl = "https://docs.qq.com/pdf/DRFVrZE9OcWFudnpZ?";
    final String htmlStr = "欢迎您使用Limited  2  Space！<br>"+
    "我们非常重视保护您的个人信息和隐私。您可以通过<a href=\"https://docs.qq.com/pdf/DRFVrZE9OcWFudnpZ?\">《Limited 2 Space隐私政策》</a>了解我们收集、使用、存储用户个人信息的情况，以及您所享有的相关权利。<br>"+
    "请您仔细阅读并充分理解相关内容： <br>"+
    "1.  为向您提供游戏服务，我们将依据<a href=\"https://docs.qq.com/pdf/DRFVrZE9OcWFudnpZ?\">《Limited 2 Space隐私政策》</a>收集、使用、存储必要的信息。<br>"+
    "2.  基于您的明示授权，虽然我们并没有相关设备权限，您有权拒绝或取消授权。<br>"+
    "3.  您可以访问、更正、删除您的个人信息，还可以撤回授权同意、注销账号、投诉举报以及调整其他隐私设置。<br>"+
    "4.  我们已采取符合业界标准的安全防护措施保护您的个人信息。<br>"+
    "5.  如您是未成年人，请您和您的监护人仔细阅读本隐私政策，并在征得您的监护人授权同意的前提下使用我们的服务或向我们提供个人信息。<br>"+
    "请您阅读完整版<a href=\"https://docs.qq.com/pdf/DRFVrZE9OcWFudnpZ?\">《Limited 2 Space隐私政策》</a>了解详细信息。<br>"+
    "如您同意<a href=\"https://docs.qq.com/pdf/DRFVrZE9OcWFudnpZ?\">《Limited 2 Space隐私政策》</a>，请您点击“同意”开始使用我们的产品和服务，我们将尽全力保护您的个人信息安全。";
 
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
 
        //如果已经同意过隐私协议则直接进入Unity Activity
        if (GetPrivacyAccept()){
            EnterUnityActivity();
            return;
        }
        ShowPrivacyDialog();//弹出隐私协议对话框
    }
 
    @Override
    public void onClick(DialogInterface dialogInterface, int i) {
        switch (i){
            case AlertDialog.BUTTON_POSITIVE://点击同意按钮
                SetPrivacyAccept(true);
                EnterUnityActivity();//启动Unity Activity
                break;
            case AlertDialog.BUTTON_NEGATIVE://点击拒绝按钮,直接退出App
                finish();
                break;
        }
    }
    private void ShowPrivacyDialog(){
        WebView webView = new WebView(this);
        if (useLocalHtml){
            webView.loadDataWithBaseURL(null, htmlStr, "text/html", "UTF-8", null);
        }else{
            webView.loadUrl(privacyUrl);
            webView.setWebViewClient(new WebViewClient(){
                @Override
                public boolean shouldOverrideUrlLoading(WebView view, String url) {
                    view.loadUrl(url);
                    return true;
                }
 
                @Override
                public void onReceivedError(WebView view, WebResourceRequest request, WebResourceError error) {
                    view.reload();
                }
 
                @Override
                public void onPageFinished(WebView view, String url) {
                    super.onPageFinished(view, url);
                }
            });
        }
 
        AlertDialog.Builder privacyDialog = new AlertDialog.Builder(this);
        privacyDialog.setCancelable(false);
        privacyDialog.setView(webView);
        privacyDialog.setTitle("温馨提示");
        privacyDialog.setNegativeButton("退出",this);
        privacyDialog.setPositiveButton("同意",this);
        privacyDialog.create().show();
    }
//启动Unity Activity
    private void EnterUnityActivity(){
        Intent unityAct = new Intent();
        unityAct.setClassName(this, "com.unity3d.player.UnityPlayerActivity");
        this.startActivity(unityAct);
    }
//保存同意隐私协议状态
    private void SetPrivacyAccept(boolean accepted){
        SharedPreferences.Editor prefs = this.getSharedPreferences("PlayerPrefs", MODE_PRIVATE).edit();
        prefs.putBoolean("PrivacyAccepted", accepted);
        prefs.apply();
    }
    private boolean GetPrivacyAccept(){
        SharedPreferences prefs = this.getSharedPreferences("PlayerPrefs", MODE_PRIVATE);
        return prefs.getBoolean("PrivacyAccepted", false);
    }
}