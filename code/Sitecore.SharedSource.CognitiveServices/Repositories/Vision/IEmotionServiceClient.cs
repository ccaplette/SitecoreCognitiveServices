﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Vision.Contract;

namespace Sitecore.SharedSource.CognitiveServices.Repositories.Vision
{
    extern alias MicrosoftProjectOxfordCommon;

    /// <summary>
    /// Stubs out the internal interface that Microsoft.ProjectOxford.Emotion.IEmotionServiceClient implements
    /// </summary>
    public interface IEmotionServiceClient
    {
        Task<Microsoft.ProjectOxford.Emotion.Contract.Emotion[]> RecognizeAsync(string imageUrl);

        Task<Microsoft.ProjectOxford.Emotion.Contract.Emotion[]> RecognizeAsync(string imageUrl, MicrosoftProjectOxfordCommon::Microsoft.ProjectOxford.Common.Rectangle[] faceRectangles);

        Task<Microsoft.ProjectOxford.Emotion.Contract.Emotion[]> RecognizeAsync(Stream imageStream);

        Task<Microsoft.ProjectOxford.Emotion.Contract.Emotion[]> RecognizeAsync(Stream imageStream, MicrosoftProjectOxfordCommon::Microsoft.ProjectOxford.Common.Rectangle[] faceRectangles);

        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(Stream videoStream);

        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(byte[] videoBytes);

        Task<VideoEmotionRecognitionOperation> RecognizeInVideoAsync(string videoUrl);

        Task<MicrosoftProjectOxfordCommon::Microsoft.ProjectOxford.Common.Contract.VideoOperationResult> GetOperationResultAsync(VideoEmotionRecognitionOperation operation);
    }
}
