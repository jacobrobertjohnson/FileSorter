using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FileSorter
{
    internal class Exif
    {
        const int PropertyTagDateTime = 0x132,
            PropertyTagExifDTOrig = 0x9003;

        public static DateTime? GetDateTaken(string filePath)
        {
            DateTime? dateTaken = null;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (Image img = Image.FromStream(fs, false, false))
            {
                dateTaken = getDateTakenFromImage(img);
            }

            return dateTaken;
        }

        private static DateTime? getDateTakenFromImage(Image img)
        {
            return getDateIfExists(img, PropertyTagExifDTOrig)
                ?? getDateIfExists(img, PropertyTagDateTime);
        }

        private static DateTime? getDateIfExists(Image img, int propertyId)
        {
            byte[] datePropBytes = searchForProperty(img, propertyId);

            if (datePropBytes != null)
                return DateConversion.ToDate(datePropBytes);

            return null;
        }

        private static byte[] searchForProperty(Image img, int propertyId)
        {
            if (img.PropertyIdList.Contains(propertyId))
                return img.GetPropertyItem(propertyId).Value;

            return null;
        }
    }
}
