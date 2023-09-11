using System.Drawing;

namespace Nucleus.Helpers {
    public static class ColorsHelper {
        public static Color RandomColor()
            => new Random().NextColor();
        
        public static Color NextColor(this Random random)
            => Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        
        public static Color Lighter(this Color input, double percent = 0.1d)
            => input.Lerp(Color.White, percent);
        
        public static Color Darker(this Color input, double percent = 0.1d)
            => input.Lerp(Color.Black, percent);
        
        public static Color Lerp(this Color input, Color shift, double percent) {
            double[] colors = {
                input.R, input.G, input.B,
                shift.R, shift.G, shift.B,
                0, 0, 0
            };
            
            // For R, G, B
            for (int c = 0; c < 3; c++) {
                double i = colors[c];
                double o = colors[c + 3];
                
                // Add the difference
                i += (o - i) * percent;
                
                // Update output
                colors[c + 6] = i;
            }
            
            return Color.FromArgb((byte) colors[6], (byte) colors[7], (byte) colors[8]);
        }
    }
}
