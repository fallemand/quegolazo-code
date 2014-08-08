<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="quegolazo_code.admin.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title></title>
    <link rel="stylesheet" href="../resources/css/bootstrap.css" />
    <link rel="stylesheet" href="/resources/css/quegolazo.css" />
   <script type="text/javascript" src="../resources/js/jquery.min.js"></script>
   <script type="text/javascript" src="../resources/js/ajaxfileupload.js"></script>
    <style type="text/css">

    </style>
</head>
    
<body>
    
   <form id="form1" runat="server">
       <asp:ScriptManager ID="MainScriptManager" runat="server" />
   <div>

       <div class="fileinput fileinput-new">
            <div class="thumbnail fileinput-preview">
                <img id="imagenpreview" src="../resources/img/theme/logo-default.png" runat="server" />
            </div>
            <div>
                <span class="btn btn-default btn-xs btn-file"><span class="fileinput-new">Seleccionar Imagen</span>
                <input id="fileToUpload" type="file" name="fileToUpload" class="upload" />
            </div>
        </div>
         <img id="loading" src="../resources/img/theme/load2.gif" style="display:none;" alt="load" />
         <span id="error"></span>
   </div>
   </form>
</body>
</html>
<script type="text/javascript">
    function ajaxFileUpload() {
        $(document).ajaxStart(function () {
            $("#loading").show();
        });
        $(document).ajaxStop(function () {
            $("#loading").hide();
        });
        $.ajaxFileUpload
        (
            {
                url: 'AjaxFileUploader.ashx',
                secureuri: false,
                fileElementId: 'fileToUpload',
                dataType: 'json',
                data: { name: 'logan', id: 'id' },
                success: function (data, status) {
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') 
                            $("#error").text(data.error);
                    }
                    else {
                        $("#error").text(data.msg);
                    }
                },
                error: function (data, status, e) {
                    alert(e);
                }
            }
        )
        return false;
    }
</script>

<script>
    $(document).ready(function () {
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imagenpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $('body').on('change', '#fileToUpload', function () {
            readURL(this);
            ajaxFileUpload();
        });
    });
</script>