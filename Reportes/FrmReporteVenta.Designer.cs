namespace SistemaVentas
{
    partial class FrmReporteVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataSetPrincipal = new SistemaVentas.DataSetPrincipal();
            this.usp_Reportes_GenerarReporteVentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usp_Reportes_GenerarReporteVentaTableAdapter = new SistemaVentas.DataSetPrincipalTableAdapters.usp_Reportes_GenerarReporteVentaTableAdapter();
            this.txtVentaId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usp_Reportes_GenerarReporteVentaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.usp_Reportes_GenerarReporteVentaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaVentas.Reportes.RptReporteVenta.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(791, 396);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSetPrincipal
            // 
            this.DataSetPrincipal.DataSetName = "DataSetPrincipal";
            this.DataSetPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // usp_Reportes_GenerarReporteVentaBindingSource
            // 
            this.usp_Reportes_GenerarReporteVentaBindingSource.DataMember = "usp_Reportes_GenerarReporteVenta";
            this.usp_Reportes_GenerarReporteVentaBindingSource.DataSource = this.DataSetPrincipal;
            // 
            // usp_Reportes_GenerarReporteVentaTableAdapter
            // 
            this.usp_Reportes_GenerarReporteVentaTableAdapter.ClearBeforeFill = true;
            // 
            // txtVentaId
            // 
            this.txtVentaId.Location = new System.Drawing.Point(12, 31);
            this.txtVentaId.Name = "txtVentaId";
            this.txtVentaId.Size = new System.Drawing.Size(100, 20);
            this.txtVentaId.TabIndex = 1;
            this.txtVentaId.Visible = false;
            // 
            // FrmReporteVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 396);
            this.Controls.Add(this.txtVentaId);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReporteVenta";
            this.Text = "FrmReporteVenta";
            this.Load += new System.EventHandler(this.FrmReporteVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usp_Reportes_GenerarReporteVentaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource usp_Reportes_GenerarReporteVentaBindingSource;
        private DataSetPrincipal DataSetPrincipal;
        private DataSetPrincipalTableAdapters.usp_Reportes_GenerarReporteVentaTableAdapter usp_Reportes_GenerarReporteVentaTableAdapter;
        private System.Windows.Forms.TextBox txtVentaId;
    }
}