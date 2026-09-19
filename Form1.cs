namespace Washing_Machine_Timer_Fuzzy_Logic;

public partial class Form1 : Form
{
    private double[] ruleStrengths = new double[4]; // r1, r2, r3, r4
    private string[] ruleNames = new string[] { "Quick", "Normal", "Deep", "Heavy" };

    public Form1()
    {
        InitializeComponent();
        cmbMethod.Items.Add("Sugeno Method");
        cmbMethod.Items.Add("Mamdani Method");
        cmbMethod.SelectedIndex = 0;

        picLoadGraph.Paint += picLoadGraph_Paint;
        picSoilingGraph.Paint += picSoilingGraph_Paint;
        picDetGraph.Paint += picDetGraph_Paint;
        picRulesGraph.Paint += picRulesGraph_Paint;

        picLoadGraph.BackColor = Color.White;
        picSoilingGraph.BackColor = Color.White;
        picDetGraph.BackColor = Color.White;
        picRulesGraph.BackColor = Color.White;
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
        double load = (double)numLoad.Value;
        double soiling = (double)numSoiling.Value;
        double detergent = (double)numDetergent.Value;

        double time = 0.0;

        if (cmbMethod.SelectedItem.ToString() == "Sugeno Method")
        {
            time = CalculateSugeno(load, soiling, detergent);
        }
        else
        {
            time = CalculateMamdani(load, soiling, detergent);
        }

        lblResult.Text = $"{time:F2} Minutes";

    }

    private void cbAutoCalc_CheckedChanged(object sender, EventArgs e)
    {
        btnCalculate.Enabled = !cbAutoCalc.Checked;
    }

    private double CalculateSugeno(double load, double soiling, double detergent)
    {
        double loadLow = TriangularMembership(load, 0, 0, 5);
        double loadMed = TriangularMembership(load, 3, 5, 7);
        double loadHigh = TrapezoidalMembership(load, 5, 7, 10, 10);

        double soilLight = TriangularMembership(soiling, 0, 0, 4);
        double soilMed = TriangularMembership(soiling, 2, 5, 8);
        double soilHeavy = TrapezoidalMembership(soiling, 5, 7, 10, 10);

        double detLow = TriangularMembership(detergent, 0, 0, 30);
        double detNormal = TriangularMembership(detergent, 20, 50, 80);
        double detHigh = TrapezoidalMembership(detergent, 60, 80, 100, 100);

        double r1 = Math.Min(loadLow, Math.Min(soilLight, detNormal));
        double cr1 = 15.0;

        double r2 = Math.Min(loadMed, Math.Min(soilMed, detNormal));
        double cr2 = 35.0;

        double r3 = Math.Max(loadHigh, Math.Max(soilHeavy, detLow));
        double cr3 = 50.0;

        double r4 = detHigh;
        double cr4 = 60.0;

        double num = (r1 * cr1) + (r2 * cr2) + (r3 * cr3) + (r4 * cr4);
        double den = r1 + r2 + r3 + r4;

        // Store current strengths
        ruleStrengths[0] = r1;
        ruleStrengths[1] = r2;
        ruleStrengths[2] = r3;
        ruleStrengths[3] = r4;

        // Repaint bar chart

        picRulesGraph.Invalidate();

        UpdateFuzzyStatusDisplay(loadLow, loadMed, loadHigh, soilLight, soilMed, soilHeavy, detLow, detNormal, detHigh, r1, r2, r3, r4);

        return den > 0 ? num / den : 0.0;
    }

    private double CalculateMamdani(double load, double soiling, double detergent)
    {
        double loadLow = TriangularMembership(load, 0, 0, 5);
        double loadMed = TriangularMembership(load, 3, 5, 7);
        double loadHigh = TrapezoidalMembership(load, 5, 7, 10, 10);

        double soilLight = TriangularMembership(soiling, 0, 0, 4);
        double soilMed = TriangularMembership(soiling, 2, 5, 8);
        double soilHeavy = TrapezoidalMembership(soiling, 5, 7, 10, 10);

        double detLow = TriangularMembership(detergent, 0, 0, 30);
        double detNormal = TriangularMembership(detergent, 20, 50, 80);
        double detHigh = TrapezoidalMembership(detergent, 60, 80, 100, 100);

        double r1 = Math.Min(loadLow, Math.Min(soilLight, detNormal));
        double r2 = Math.Min(loadMed, Math.Min(soilMed, detNormal));
        double r3 = Math.Min(loadHigh, Math.Min(soilHeavy, detHigh));
        double r4 = detHigh;

        double sumNum = 0.0;
        double sumDen = 0.0;
        double step = 1.0;

        for (double t = 0.0; t <= 60.0; t += step)
        {
            double outShort = TriangularMembership(t, 0, 0, 25.0);
            double outMed = TriangularMembership(t, 20.0, 35.0, 50.0);
            double outLong = TriangularMembership(t, 35.0, 50.0, 65.0);
            double outExtra = TriangularMembership(t, 50.0, 60.0, 60.0);

            double clip1 = Math.Min(r1, outShort);
            double clip2 = Math.Min(r2, outMed);
            double clip3 = Math.Min(r3, outLong);
            double clip4 = Math.Min(r4, outExtra);

            double aggregated = Math.Max(clip1, Math.Max(clip2, Math.Max(clip3, clip4)));

            sumNum += t * aggregated * step;
            sumDen += aggregated * step;
        }

        // Store current strengths
        ruleStrengths[0] = r1;
        ruleStrengths[1] = r2;
        ruleStrengths[2] = r3;
        ruleStrengths[3] = r4;

        // Repaint bar chart

        picRulesGraph.Invalidate();
        UpdateFuzzyStatusDisplay(loadLow, loadMed, loadHigh, soilLight, soilMed, soilHeavy, detLow, detNormal, detHigh, r1, r2, r3, r4);

        return sumDen > 0 ? sumNum / sumDen : 0.0;
    }


    private double TriangularMembership(double x, double a, double b, double c)
    {
        if (x <= a || x >= c) return 0.0;
        if (x == b) return 1.0;
        if (x > a && x < b) return (x - a) / (b - a);
        return (c - x) / (c - b);
    }

    private double TrapezoidalMembership(double x, double a, double b, double c, double d)
    {
        if (x < a || x > d) return 0.0;
        if (x >= b && x <= c) return 1.0;
        if (x > a && x < b) return (x - a) / (b - a);
        return (d - x) / (d - c);
    }

    private void numLoad_Scroll(object sender, EventArgs e)
    {
        lblLoad.Text = $"{numLoad.Value} kg";
        picLoadGraph.Invalidate(); // Triggers repainting of the load graph

        if (cbAutoCalc.Checked)
        {
            btnCalculate_Click(sender, e);
        }
    }

    private void numSoiling_Scroll(object sender, EventArgs e)
    {
        lblSoilingLevel.Text = $"{numSoiling.Value} / 10";
        picSoilingGraph.Invalidate();

        if (cbAutoCalc.Checked)
        {
            btnCalculate_Click(sender, e);
        }
    }

    private void numDetergent_Scroll(object sender, EventArgs e)
    {
        lblDetAmount.Text = $"{numDetergent.Value} ml";
        picDetGraph.Invalidate();

        if (cbAutoCalc.Checked)
        {
            btnCalculate_Click(sender, e);
        }
    }

    private void picLoadGraph_Paint(object sender, PaintEventArgs e)
    {
        PictureBox box = sender as PictureBox;
        var loadShapes = new List<PointF[]>
        {
            new PointF[] { new PointF(0, 1), new PointF(5, 0) },
            new PointF[] { new PointF(3, 0), new PointF(5, 1), new PointF(7, 0) },
            new PointF[] { new PointF(5, 0), new PointF(7, 1), new PointF(18, 1) }
        };
        string[] labels = { "Low", "Med", "High" };

        DrawMembershipGraph(box, e.Graphics, numLoad.Value, 18, "Load Membership (kg)",
            new Color[] { Color.Blue, Color.Green, Color.Goldenrod }, loadShapes, labels);
    }

    private void picSoilingGraph_Paint(object sender, PaintEventArgs e)
    {
        PictureBox box = sender as PictureBox;
        var soilShapes = new List<PointF[]>
        {
            new PointF[] { new PointF(0, 1), new PointF(4, 0) },
            new PointF[] { new PointF(2, 0), new PointF(5, 1), new PointF(8, 0) },
            new PointF[] { new PointF(5, 0), new PointF(7, 1), new PointF(10, 1) }
        };
        string[] labels = { "Light", "Med", "Heavy" };

        DrawMembershipGraph(box, e.Graphics, numSoiling.Value, 10, "Soiling Level Membership",
            new Color[] { Color.Blue, Color.Green, Color.Goldenrod }, soilShapes, labels);
    }

    private void picDetGraph_Paint(object sender, PaintEventArgs e)
    {
        PictureBox box = sender as PictureBox;
        var detShapes = new List<PointF[]>
        {
            new PointF[] { new PointF(0, 1), new PointF(30, 0) },
            new PointF[] { new PointF(20, 0), new PointF(50, 1), new PointF(80, 0) },
            new PointF[] { new PointF(60, 0), new PointF(80, 1), new PointF(100, 1) }
        };
        string[] labels = { "Low", "Normal", "High" };

        DrawMembershipGraph(box, e.Graphics, numDetergent.Value, 100, "Detergent Membership (ml)",
            new Color[] { Color.Blue, Color.Green, Color.Goldenrod }, detShapes, labels);
    }

    private void DrawMembershipGraph(PictureBox box, Graphics g, double currentValue, double maxX, string title, Color[] colors, List<PointF[]> shapes, string[] labels)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int width = box.Width;
        int height = box.Height;

        // Optimized padding to maximize graph drawing space
        int leftPadding = 28;
        int rightPadding = 28;
        int topPadding = 25;
        int bottomPadding = 18;

        int graphWidth = width - leftPadding - rightPadding;
        int graphHeight = height - topPadding - bottomPadding;

        // Draw Title
        g.DrawString(title, new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, leftPadding, 2);

        // Axes lines
        int originY = height - bottomPadding;
        g.DrawLine(Pens.Black, leftPadding, originY, leftPadding + graphWidth, originY); // X-axis
        g.DrawLine(Pens.Black, leftPadding, topPadding, leftPadding, originY); // Y-axis

        // Draw Y-Axis Labels
        g.DrawString("1", new Font("Segoe UI", 7), Brushes.Black, leftPadding - 18, topPadding - 2);
        g.DrawString("0", new Font("Segoe UI", 7), Brushes.Black, leftPadding - 15, originY - 8);

        // X-Axis Max Label
        g.DrawString(maxX.ToString(), new Font("Segoe UI", 7), Brushes.Black, leftPadding + graphWidth - 10, originY + 2);

        // Mapping helper functions
        Func<double, double, float> mapX = (val, max) => (float)(leftPadding + (val / max) * graphWidth);
        Func<double, float> mapY = (val) => (float)(originY - (val * graphHeight));

        // Draw Membership Curves (Triangles/Trapezoids)
        for (int i = 0; i < shapes.Count; i++)
        {
            PointF[] dataPoints = shapes[i];
            PointF[] screenPoints = new PointF[dataPoints.Length];

            for (int j = 0; j < dataPoints.Length; j++)
            {
                screenPoints[j] = new PointF(mapX(dataPoints[j].X, maxX), mapY(dataPoints[j].Y));
            }

            using (Pen pen = new Pen(colors[i % colors.Length], 2))
            {
                g.DrawLines(pen, screenPoints);
            }
        }

        // Draw Current Value Vertical Line (Red Line)
        float currentX = mapX(Math.Min(currentValue, maxX), (float)maxX);
        g.DrawLine(new Pen(Color.Red, 2), currentX, topPadding, currentX, originY);
    }

    private void DrawRuleFiringGraph(PictureBox box, Graphics g, double[] strengths, string[] names)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int width = box.Width;
        int height = box.Height;

        int leftPadding = 38;
        int rightPadding = 20;
        int topPadding = 25;
        int bottomPadding = 45; // Room for rotated or angled labels

        int graphWidth = width - leftPadding - rightPadding;
        int graphHeight = height - topPadding - bottomPadding;
        int originY = height - bottomPadding;

        // Title
        g.DrawString("Rule Firing Strengths", new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Black, leftPadding - 10, 5);

        // Y-Axis grid lines & labels (0.0 to 1.0)
        Font labelFont = new Font("Segoe UI", 7);
        for (double v = 0.0; v <= 1.0; v += 0.2)
        {
            float y = (float)(originY - (v * graphHeight));
            // Light grid line
            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                g.DrawLine(gridPen, leftPadding, y, leftPadding + graphWidth, y);
            }
            g.DrawString(v.ToString("0.0"), labelFont, Brushes.Black, leftPadding - 28, y - 6);
        }

        // Main Axes
        g.DrawLine(Pens.Black, leftPadding, originY, leftPadding + graphWidth, originY); // X-axis
        g.DrawLine(Pens.Black, leftPadding, topPadding, leftPadding, originY);            // Y-axis

        // Draw Bars
        int barCount = strengths.Length;
        float slotWidth = (float)graphWidth / barCount;
        float barWidth = slotWidth * 0.55f;

        for (int i = 0; i < barCount; i++)
        {
            double val = Math.Clamp(strengths[i], 0.0, 1.0);
            float barH = (float)(val * graphHeight);
            float x = leftPadding + (i * slotWidth) + (slotWidth - barWidth) / 2;
            float y = originY - barH;

            // Fill bar (black or dark navy like the reference)
            using (Brush barBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(barBrush, x, y, barWidth, barH);
            }

            // Rule name below the bar
            string label = $"R{i + 1}\n({names[i]})";
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };
            g.DrawString(label, labelFont, Brushes.Black, new RectangleF(leftPadding + (i * slotWidth), originY + 4, slotWidth, bottomPadding - 4), sf);
        }
    }

    private void picRulesGraph_Paint(object sender, PaintEventArgs e)
    {
        DrawRuleFiringGraph(picRulesGraph, e.Graphics, ruleStrengths, ruleNames);
    }

    private void UpdateFuzzyStatusDisplay(
        double loadLow, double loadMed, double loadHigh,
        double soilLight, double soilMed, double soilHeavy,
        double detLow, double detNormal, double detHigh,
        double r1, double r2, double r3, double r4)
    {
        // Input Memberships
        lblInLoadLow.Text = $"Load Low: {loadLow:F2}";
        lblInLoadMed.Text = $"Load Med: {loadMed:F2}";
        lblInLoadHigh.Text = $"Load High: {loadHigh:F2}";

        lblInSoilLight.Text = $"Soil Light: {soilLight:F2}";
        lblInSoilMed.Text = $"Soil Med: {soilMed:F2}";
        lblInSoilHeavy.Text = $"Soil Heavy: {soilHeavy:F2}";

        lblInDetLow.Text = $"Det Low: {detLow:F2}";
        lblInDetNorm.Text = $"Det Normal: {detNormal:F2}";
        lblInDetHigh.Text = $"Det High: {detHigh:F2}";

        // Rule Firing Strengths
        lblR1.Text = $"R1 (Quick): {r1:F2}";
        lblR2.Text = $"R2 (Normal): {r2:F2}";
        lblR3.Text = $"R3 (Deep Wash): {r3:F2}";
        lblR4.Text = $"R4 (Heavy Duty): {r4:F2}";
    }


}
